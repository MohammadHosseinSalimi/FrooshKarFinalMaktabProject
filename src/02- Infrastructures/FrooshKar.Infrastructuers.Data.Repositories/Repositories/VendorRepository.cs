using System.Runtime.CompilerServices;
using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class VendorRepository : IVendorRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly UserManager<AppUser> _userManager;

		public VendorRepository(FrooshKarDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_contextAccessor = contextAccessor;
			_userManager = userManager;
		}

		public async Task Create(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Vendor>(entity);
			await _dbContext.Vendors.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<VendorDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Vendors.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<VendorDtoModel>>(record);

		}
		public async Task<List<VendorDtoModel>> GetAllWithProducts(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Vendors.Include(x=>x.BidProducts).Include(x=>x.FixedPriceProducts).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<VendorDtoModel>>(record);

		}
		public async Task<VendorDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Vendors
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<VendorDtoModel>(record);
		}

		public async Task Update(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			//todo: why tracking doesn't work (this problem has been solved but I must copy this code to vendor dto model)
			var record = await _dbContext.Vendors.AsNoTracking().Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			var vendor = _mapper.Map(entity, record);
			_dbContext.Update(vendor);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}
		public async Task<VendorDtoModel> VendorWagePercentFromFixedPriceProduct(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Vendors.Include(x => x.FixedPriceProducts).ThenInclude(x => x.Carts).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return _mapper.Map<VendorDtoModel>(record);

        }

        public async Task<VendorDtoModel> VendorWagePercentFromBidProduct(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Vendors.Include(x => x.BidProducts).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<VendorDtoModel>(record);

		}
		public async Task<int> FindCurrentVendorId(CancellationToken cancellationToken)
		{

			var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

			var record = await _dbContext.Vendors.AsNoTracking().FirstOrDefaultAsync(x => x.AppUserId == currentUser.Id, cancellationToken);
			return record.Id;
		}


		public async Task CreateByAppUser(VendorDtoModel entity, int appUserId, CancellationToken cancellationToken)
		{

			await _dbContext.Vendors.AddAsync(_mapper.Map<Vendor>(entity), cancellationToken);
			await Save(cancellationToken);

		}





		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
