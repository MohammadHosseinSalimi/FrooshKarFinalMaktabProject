using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Service
{
	public interface IFixedPriceProductService
	{
		Task Create(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);
		Task<List<FixedPriceProductDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<FixedPriceProductDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);

		Task ValidationByAdmin(FixedPriceProductDtoModel entity, CancellationToken cancellationToken);

        public Task<List<FixedPriceProductDtoModel>> GetAllByVendorId(int id, CancellationToken cancellationToken);



    }
}
