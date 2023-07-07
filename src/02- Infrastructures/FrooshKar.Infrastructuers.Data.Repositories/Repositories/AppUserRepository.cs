using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Identity;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class AppUserRepository : IAppUserRepository
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMapper _mapper;
		private readonly FrooshKarDbContext _dbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AppUserRepository(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			IMapper mapper,
			FrooshKarDbContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_dbContext = dbContext;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> IsExist(string emailAddress, CancellationToken cancellationToken)
		{
			var record = await _userManager.Users
				.Where(x => x.NormalizedEmail == emailAddress.ToUpper())
				.FirstOrDefaultAsync(cancellationToken);

			if (record != null)
				return true;

			return false;
		}


		public async Task<int> Create(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			int userId = 0;
			var user = new AppUser()
			{
				UserName = command.Email,
				Email = command.Email
			};

			var result = await _userManager.CreateAsync(user, command.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, command.Role);



				var rolesAsync = await _userManager.GetRolesAsync(user);
				userId = Convert.ToInt32(await _userManager.GetUserIdAsync(user));
				switch (rolesAsync.FirstOrDefault())
				{
					case "Customer":
					{
						var customer = new Customer()
						{
							AppUserId = userId
						};
						break;
					}
					case "Vendor":
					{
						var vendor = new Vendor()
						{
							AppUserId = userId

						};
						break;
					}
				}

			}

			return userId;
		}

		public async Task<List<AppUserDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			var users = _mapper.Map<List<AppUserDtoModel>>(await _userManager.Users
				.AsNoTracking()
				.ToListAsync(cancellationToken));

			foreach (var item in users)
			{
				var userRole =
					await _userManager.GetRolesAsync(
						await _userManager.Users.FirstAsync(x => x.Id == item.Id, cancellationToken));
				item.Role = userRole.First();
			}

			return users;
		}


		public async Task<SignInResult> Login(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			return await _signInManager.PasswordSignInAsync(command.Email, command.Password, true, false);
		}

		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<int> GetCurrentUserId()
		{
			var record = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			return record.Id;
		}

		public async Task DeleteUser(int userId, CancellationToken cancellationToken)
		{
			var record = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
			await _userManager.SetLockoutEnabledAsync(record, true);

			//await _userManager.DeleteAsync(record);
			record.IsDeleted = true;
			await Save(cancellationToken);
		}


		public async Task UpdateUser(AppUserDtoModel entity, CancellationToken cancellationToken)
		{
			var record = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
			_mapper.Map(entity, record);
			//await _userManager.Users.ExecuteUpdateAsync(x=>x.SetProperty(u=>u.IsDeleted,p=>true));
			//await _userManager.Users.ExecuteDeleteAsync()

			await Save(cancellationToken);

		}



		public async Task<AppUserDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<AppUserDtoModel>(record);
		}

		public async Task<string> FindUserRoleByEmail(string email, CancellationToken cancellationToken)
		{
			
			var user=await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
			var roles = await _userManager.GetRolesAsync(user);
			return roles.FirstOrDefault();

		}




		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);

		}



	}

}
