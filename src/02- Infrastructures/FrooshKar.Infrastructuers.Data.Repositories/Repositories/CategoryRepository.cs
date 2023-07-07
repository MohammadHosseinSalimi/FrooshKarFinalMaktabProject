using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly UserManager<AppUser> _userManager;

		public CategoryRepository(FrooshKarDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_contextAccessor = contextAccessor;
			_userManager = userManager;
		}

		public async Task Create(CategoryDtoModel entity, CancellationToken cancellationToken)
		{

			entity.CreatedAt = DateTime.Now;
			var findCurrentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
			//entity.CreatedBy = findCurrentUser.Id;

			var record = _mapper.Map<Category>(entity);
			await _dbContext.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<CategoryDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			var record = await _dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CategoryDtoModel>>(record);	
		}

		public async Task<CategoryDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Categories
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<CategoryDtoModel>(record);
		}

		public async Task Update(CategoryDtoModel entity, CancellationToken cancellationToken)
		{
			entity.LastModifiedAt = DateTime.Now;
			var findCurrentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
	//		entity.LastModifiedBy = findCurrentUser.Id;

			//var mapping = _mapper.Map<Category>(entity);
			var record = await _dbContext.Categories.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{
			var findCurrentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

			var record = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			record.DeletedAt = DateTime.Now;
	//		record.DeletedBy = findCurrentUser.Id;
			await Save(cancellationToken);
		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
