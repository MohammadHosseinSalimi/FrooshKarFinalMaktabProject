using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class FactorRepository: IFactorRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public FactorRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(FactorDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Factor>(entity);
			await _dbContext.Factors.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<FactorDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Factors.Include(x=>x.Comment).Include(x=>x.Carts).ThenInclude(c=>c.FixedPriceProduct).ThenInclude(x=>x.ProductImages).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<FactorDtoModel>>(record);


		}

		public async Task<FactorDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Factors
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<FactorDtoModel>(record);
		}

		public async Task Update(FactorDtoModel entity, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Factors.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Factors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}

		public async Task<List<FactorDtoModel>> GetAllWithVendor(CancellationToken cancellationToken)
		{
			var record = await _dbContext.Factors.Include(x => x.Carts).ThenInclude(x => x.FixedPriceProduct)
				.ThenInclude(x => x.Vendor).AsNoTracking().ToListAsync(cancellationToken);

			return _mapper.Map<List<FactorDtoModel>>(record);
		}


		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
