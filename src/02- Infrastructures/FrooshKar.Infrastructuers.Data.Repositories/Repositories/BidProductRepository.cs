using System.Data;
using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class BidProductRepository : IBidProductRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public BidProductRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<BidProduct>(entity);
			await _dbContext.BidProducts.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<BidProductDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.BidProducts.AsNoTracking().Include(x=>x.ProductImages).Include(x=>x.Category).Include(x=>x.Vendor).ToListAsync(cancellationToken);
			return _mapper.Map<List<BidProductDtoModel>>(record);


		}


        public async Task<List<BidProductDtoModel>> GetAllbyVendorId(int id, CancellationToken cancellationToken)
        {
            var record = await _dbContext.BidProducts.Include(x=>x.Category).AsNoTracking().Where(x => x.VendorId == id)
                .ToListAsync(cancellationToken);
            var mapping = _mapper.Map<List<BidProductDtoModel>>(record);
            return mapping;
        }



        public async Task<BidProductDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.BidProducts
				.Include(x=>x.Vendor)
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<BidProductDtoModel>(record);
		}

		public async Task Update(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			var record = await _dbContext.BidProducts.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.BidProducts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}
		public async Task ValidationByAdmin(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			var mapping = _mapper.Map<BidProduct>(entity);
			var record = await _dbContext.BidProducts.FirstOrDefaultAsync(x=>x.Id==mapping.Id,cancellationToken);
			record.IsValidByAdmin = true;
			await Save(cancellationToken);
		}


        public async Task<BidProductDtoModel> GetBidProductAndBid(int id, CancellationToken cancellationToken)
        {
            var record = await _dbContext.BidProducts.AsNoTracking().Include(x => x.Bids)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var mapping = _mapper.Map<BidProductDtoModel>(record);
            return mapping;

        }




		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}






	}
}
