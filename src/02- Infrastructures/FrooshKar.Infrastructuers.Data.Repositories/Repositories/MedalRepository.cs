using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class MedalRepository: IMedalRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public MedalRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(MedalDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Medal>(entity);
			await _dbContext.Medals.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<MedalDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Medals.AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<MedalDtoModel>>(record);


		}

		public async Task<MedalDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Medals
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<MedalDtoModel>(record);
		}

		public async Task Update(MedalDtoModel entity, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Medals.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Medals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}










		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
