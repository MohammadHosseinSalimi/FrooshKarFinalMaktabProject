using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
	public class CommentRepository: ICommentRepository
	{
		private readonly FrooshKarDbContext _dbContext;
		private readonly IMapper _mapper;

		public CommentRepository(FrooshKarDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task Create(CommentDtoModel entity, CancellationToken cancellationToken)
		{
			var record = _mapper.Map<Comment>(entity);
			await _dbContext.Comments.AddAsync(record, cancellationToken);
			await Save(cancellationToken);

		}

		public async Task<List<CommentDtoModel>> GetAll(CancellationToken cancellationToken)
		{

			var record = await _dbContext.Comments.Include(x=>x.Factor).ThenInclude(x=>x.Carts).ThenInclude(x=>x.Customer).Include(x=>x.Factor).ThenInclude(x=>x.Carts).ThenInclude(x=>x.FixedPriceProduct).ThenInclude(x=>x.Vendor).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CommentDtoModel>>(record);


		}

		public async Task<CommentDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Comments
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			return _mapper.Map<CommentDtoModel>(record);
		}

		public async Task Update(CommentDtoModel entity, CancellationToken cancellationToken)
		{
			var record = await _dbContext.Comments.Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
			_mapper.Map(entity, record);
			await Save(cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{

			var record = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
			record.IsDeleted = true;
			await Save(cancellationToken);


		}

		public async Task Save(CancellationToken cancellationToken)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task ValidationByAdmin(CommentDtoModel entity, CancellationToken cancellationToken)
		{
			var mapping = _mapper.Map<CommentDtoModel>(entity);
			var record = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == mapping.Id, cancellationToken);
			record.IsValidByAdmin = true;
			await Save(cancellationToken);
		}

		public async Task<List<CommentDtoModel>> GetAllCommentsWithVendors(CancellationToken cancellationToken)
		{
			var record = await _dbContext.Comments.Include(x => x.Factor).ThenInclude(x => x.Carts)
				.ThenInclude(x => x.FixedPriceProduct).ThenInclude(x => x.Vendor).AsNoTracking().ToListAsync(cancellationToken);
			return _mapper.Map<List<CommentDtoModel>>(record);
		}


	}
}
