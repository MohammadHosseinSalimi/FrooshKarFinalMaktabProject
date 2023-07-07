using AutoMapper;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructuers.Data.Repositories.Repositories
{
    public class ProductImageRepository: IProductImageRepository
	{
	    private readonly FrooshKarDbContext _dbContext;
	    private readonly IMapper _mapper;

	    public ProductImageRepository(FrooshKarDbContext dbContext, IMapper mapper)
	    {
		    _dbContext = dbContext;
		    _mapper = mapper;
	    }

	    public async Task Create(ProductImageDtoModel entity, CancellationToken cancellationToken)
	    {
		    var record = _mapper.Map<ProductImage>(entity);
		    await _dbContext.ProductImages.AddAsync(record, cancellationToken);
		    await Save(cancellationToken);

	    }

	    public async Task<List<ProductImageDtoModel>> GetAll(CancellationToken cancellationToken)
	    {

		    var record = await _dbContext.ProductImages.AsNoTracking().ToListAsync(cancellationToken);
		    return _mapper.Map<List<ProductImageDtoModel>>(record);


	    }

	    public async Task<ProductImageDtoModel> GetById(int id, CancellationToken cancellationToken)
	    {
		    var record = await _dbContext.ProductImages
			    .AsNoTracking()
			    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		    return _mapper.Map<ProductImageDtoModel>(record);
	    }

	    public async Task Update(ProductImageDtoModel entity, CancellationToken cancellationToken)
	    {
		    var mapping = _mapper.Map<ProductImage>(entity);
		    var record = await _dbContext.ProductImages.Where(x => x.Id == mapping.Id).FirstOrDefaultAsync(cancellationToken);
		    _mapper.Map(mapping, record);
		    await Save(cancellationToken);
	    }

	    public async Task Delete(int id, CancellationToken cancellationToken)
	    {

		    var record = await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
		    record.IsDeleted = true;
		    await Save(cancellationToken);


	    }

	    public async Task Save(CancellationToken cancellationToken)
	    {
		    await _dbContext.SaveChangesAsync(cancellationToken);
	    }
	}
}
