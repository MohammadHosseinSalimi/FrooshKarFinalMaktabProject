using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface IVendorAppService
	{
		Task Create(VendorDtoModel entity, CancellationToken cancellationToken);
		Task<List<VendorDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<VendorDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(VendorDtoModel entity, CancellationToken cancellationToken);

		Task Delete(int id, CancellationToken cancellationToken);

		public Task<int> FindCurrentVendorId(CancellationToken cancellationToken);
		public Task<double> FindVendorWageForEachFactor(int id, int factorId, CancellationToken cancellationToken);
		public Task UpdateVendorMedal(int id, CancellationToken cancellationToken);

		public Task<double> FindVendorWageForEachBidProduct(int vendorId, int bidProductId,
			CancellationToken cancellationToken);
		public Task CreateByAppUser(VendorDtoModel entity, int appUserId, CancellationToken cancellationToken);
		public Task<List<VendorDtoModel>> GetAllWithProducts(CancellationToken cancellationToken);


	}
}
