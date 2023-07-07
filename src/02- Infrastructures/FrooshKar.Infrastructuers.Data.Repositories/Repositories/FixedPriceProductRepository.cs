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
	public class FixedPriceProductRepository : IFixedPriceProductRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;



		public FixedPriceProductRepository(FrooshKarDbContext dbContext, IMapper mapper, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}

		public async Task Create(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
		{
			entity.CreatedAt = DateTime.Now;
			var findCurrentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
			//entity.CreatedBy = findCurrentUser.Id;

			var record = _mapper.Map<FixedPriceProduct>(entity);
			await _dbContext.FixedPriceProducts.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<FixedPriceProductDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.FixedPriceProducts.Include(c=>c.Carts).Include(x => x.ProductImages).Include(x => x.Category).Include(x=>x.Vendor).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<FixedPriceProductDtoModel>>(record);


		}

		public async Task<FixedPriceProductDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.FixedPriceProducts
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<FixedPriceProductDtoModel>(record);
		}

		public async Task Update(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
		{
			entity.LastModifiedAt = DateTime.Now;
			var findCurrentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
			//		entity.LastModifiedBy = findCurrentUser.Id;

			var record = await _dbContext.FixedPriceProducts.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.FixedPriceProducts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			record.DeletedAt = DateTime.Now;
			//		record.DeletedBy = findCurrentUser.Id;
			await Save(cancellationToken);


		}
		public async Task ValidationByAdmin(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
		{
			var mapping = _mapper.Map<FixedPriceProductDtoModel>(entity);
			var record = await _dbContext.FixedPriceProducts.FirstOrDefaultAsync(x => x.Id == mapping.Id, cancellationToken);
			record.IsValidByAdmin = true;
			await Save(cancellationToken);
		}

		public async Task<List<FixedPriceProductDtoModel>> GetAllbyVendorId(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.FixedPriceProducts.Include(x=>x.Category).AsNoTracking().Where(x => x.VendorId == id)
				.ToListAsync(cancellationToken);
			var mapping = _mapper.Map<List<FixedPriceProductDtoModel>>(record);
			return mapping;
		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
