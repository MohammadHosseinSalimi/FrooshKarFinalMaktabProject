using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class CityRepository: ICityRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public CityRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(CityDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<City>(entity);
			await _dbContext.Cities.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<CityDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Cities.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CityDtoModel>>(record);


		}

		public async Task<CityDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Cities
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<CityDtoModel>(record);
		}

		public async Task Update(CityDtoModel entity, CancellationToken cancellationToken)
		{
			var mapping = _mapper.Map<City>(entity);
			var record = await _dbContext.Cities.Where(x => x.Id == mapping.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(mapping, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Cities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}




	}
}
