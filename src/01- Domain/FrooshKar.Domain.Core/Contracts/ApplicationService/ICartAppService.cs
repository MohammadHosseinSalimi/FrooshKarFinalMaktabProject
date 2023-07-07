using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface ICartAppService
	{
		Task Create(CartDtoModel entity, CancellationToken cancellationToken);
		Task<List<CartDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CartDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CartDtoModel entity, CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);

        public Task FinalizeCartAndConvertToFactor(CartDtoModel entity, int currentCustomerId,
            CancellationToken cancellationToken);


    }
}
