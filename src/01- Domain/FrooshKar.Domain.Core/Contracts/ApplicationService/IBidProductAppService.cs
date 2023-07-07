using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface IBidProductAppService
	{
		Task Create(BidProductDtoModel entity, CancellationToken cancellationToken);
		Task<List<BidProductDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<BidProductDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(BidProductDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
        public Task<List<BidProductDtoModel>> GetAllByVendorId(int id, CancellationToken cancellationToken);

        Task ValidationByAdmin(BidProductDtoModel entity, CancellationToken cancellationToken);
        public Task<bool> AddCustomerBidToProduct(int bidProductId, int? price, CancellationToken cancellationToken);
        public Task SellBidProduct(BidProductDtoModel entity, CancellationToken cancellationToken);
		public Task OpenBidProduct(BidProductDtoModel entity, CancellationToken cancellationToken);
		public Task SellProductWithDelay(BidProductDtoModel entity, CancellationToken cancellationToken);
		public Task OpenBidProductWithDelay(BidProductDtoModel entity, CancellationToken cancellationToken);


	}
}
