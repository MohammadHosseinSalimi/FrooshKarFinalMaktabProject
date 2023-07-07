using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class CartRepository: ICartRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public CartRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(CartDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Cart>(entity);
			await _dbContext.Carts.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<CartDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Carts.Include(x=>x.FixedPriceProduct).ThenInclude(x=>x.ProductImages).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CartDtoModel>>(record);


		}

		public async Task<CartDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Carts
				.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<CartDtoModel>(record);
		}

		public async Task Update(CartDtoModel entity, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Carts.AsNoTracking().Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			_dbContext.Carts.Update(record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Carts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}



	}
}
