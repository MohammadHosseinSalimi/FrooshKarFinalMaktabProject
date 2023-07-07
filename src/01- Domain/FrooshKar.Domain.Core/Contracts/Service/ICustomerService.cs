using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Core.Contracts.Service
{
	public interface ICustomerService
	{
		Task CreateByAppUser(CustomerDtoModel entity, int appUserId, CancellationToken cancellationToken);
		Task<List<CustomerDtoModel>> GetAll(CancellationToken cancellationToken);
		Task<CustomerDtoModel> GetById(int id, CancellationToken cancellationToken);
		Task Update(CustomerDtoModel entity, CancellationToken cancellationToken);
        public Task<int> FindCurrentCustomerId(CancellationToken cancellationToken);

        Task Delete(int id, CancellationToken cancellationToken);
	}
}
