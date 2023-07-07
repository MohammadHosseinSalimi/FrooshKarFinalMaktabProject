using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Repository
{
	public interface ICartRepository
	{
		Task Create(CartDtoModel entity, CancellationToken cancellationToken);
		Task<List<CartDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CartDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CartDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Save(CancellationToken cancellationToken);
	}
}
