using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface IFixedPriceProductRepository
	{
		Task Create(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);
		Task<List<FixedPriceProductDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<FixedPriceProductDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);
		Task ValidationByAdmin(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);
        public Task<List<FixedPriceProductDtoModel>> GetAllbyVendorId(int id, CancellationToken cancellationToken);


    }
}
