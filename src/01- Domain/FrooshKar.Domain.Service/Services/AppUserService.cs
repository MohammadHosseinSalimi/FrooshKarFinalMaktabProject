using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FrooshKar.Domain.Service.Services
{
	public class AppUserService : IAppUserService
	{
		private readonly IAppUserRepository _userRepository;

		public AppUserService(IAppUserRepository userRepository)
		{
			_userRepository = userRepository;
		}




		public async Task<bool> IsExist(string emailAddress, CancellationToken cancellationToken)
		{
			return await _userRepository.IsExist(emailAddress,cancellationToken);
		}

		public async Task<int> Create(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			return await _userRepository.Create(command, cancellationToken);
		}

		public async Task<List<AppUserDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _userRepository.GetAll(cancellationToken);
		}

		public async Task<SignInResult> Login(AppUserDtoModel command, CancellationToken cancellationToken)
		{
			return await _userRepository.Login(command, cancellationToken);
		}


		public async Task Logout()
		{
			await _userRepository.Logout();
		}

		public async Task<int> GetCurrentUserId()
		{
			return await _userRepository.GetCurrentUserId();
		}

		public async Task UpdateUser(AppUserDtoModel entity, CancellationToken cancellationToken)
		{
			 await _userRepository.UpdateUser(entity, cancellationToken);
		}

		public async Task<string> FindUserRoleByEmail(string email, CancellationToken cancellationToken)
		{
			return await _userRepository.FindUserRoleByEmail(email, cancellationToken);
		}

		public async Task DeleteUser(int userId, CancellationToken cancellationToken)
		{
			await _userRepository.DeleteUser(userId, cancellationToken);
		}
		public async Task<AppUserDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _userRepository.GetById(id, cancellationToken);
		}
		public async Task Save(CancellationToken cancellationToken)
		{
			await _userRepository.Save(cancellationToken);
		}
	}
}
