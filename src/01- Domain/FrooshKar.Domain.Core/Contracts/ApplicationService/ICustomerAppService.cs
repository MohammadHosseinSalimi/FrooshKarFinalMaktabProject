using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.ApplicationService
{
	public interface ICustomerAppService
	{
		Task CreateByAppUser(CustomerDtoModel entity, int appUserId, CancellationToken cancellationToken);
		Task<List<CustomerDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CustomerDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CustomerDtoModel entity, CancellationToken cancellationToken);
        public Task<int> FindCurrentCustomerId(CancellationToken cancellationToken);
        public Task CreateCart(int CustomerId, CancellationToken cancellationToken);


		Task Delete(int id, CancellationToken cancellationToken);
	}
}
