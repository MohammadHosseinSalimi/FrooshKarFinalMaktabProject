using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class BidRepository:IBidRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public BidRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(BidDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Bid>(entity);
			await _dbContext.Bids.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<BidDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			
			var record = await _dbContext.Bids.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<BidDtoModel>>(record);

			
		}

		public async Task<BidDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Bids
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<BidDtoModel>(record);
		}

		public async Task Update(BidDtoModel entity, CancellationToken cancellationToken)
		{
			var mapping = _mapper.Map<Bid>(entity);
			var record = await _dbContext.Bids.Where(x => x.Id == mapping.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(mapping, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Bids.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
