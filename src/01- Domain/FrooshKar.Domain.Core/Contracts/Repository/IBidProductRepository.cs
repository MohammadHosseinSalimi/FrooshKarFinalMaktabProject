using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface IBidProductRepository
	{

		Task Create(BidProductDtoModel entity, CancellationToken cancellationToken);
		Task<List<BidProductDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<BidProductDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(BidProductDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);
        public Task<List<BidProductDtoModel>> GetAllbyVendorId(int id, CancellationToken cancellationToken);
        public Task<BidProductDtoModel> GetBidProductAndBid(int id, CancellationToken cancellationToken);


		Task ValidationByAdmin(BidProductDtoModel entity, CancellationToken cancellationToken);










	}
}
