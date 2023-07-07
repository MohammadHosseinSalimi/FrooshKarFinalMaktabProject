using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface IVendorRepository
	{
		Task Create(VendorDtoModel entity, CancellationToken cancellationToken);
		Task<List<VendorDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<VendorDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(VendorDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		public Task<int> FindCurrentVendorId(CancellationToken cancellationToken);

		public Task<VendorDtoModel> VendorWagePercentFromFixedPriceProduct(int id, CancellationToken cancellationToken);


		public  Task<VendorDtoModel> VendorWagePercentFromBidProduct(int id, CancellationToken cancellationToken);

		public Task CreateByAppUser(VendorDtoModel entity, int appUserId, CancellationToken cancellationToken);
		public  Task<List<VendorDtoModel>> GetAllWithProducts(CancellationToken cancellationToken);

		Task Save(CancellationToken cancellationToken);
	}
}
