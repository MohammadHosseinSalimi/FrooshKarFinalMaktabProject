using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Service
{
	public interface IBidService
	{
		Task Create(BidDtoModel entity, CancellationToken cancellationToken);
		Task<List<BidDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<BidDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(BidDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);









	}
}
