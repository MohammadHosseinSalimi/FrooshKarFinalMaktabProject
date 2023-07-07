using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task CreateByAppUser(CustomerDtoModel entity, int appUserId, CancellationToken cancellationToken)
        {
            await _customerRepository.CreateByAppUser(entity,appUserId, cancellationToken);
        }

        public async Task<List<CustomerDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAll(cancellationToken);
        }

        public async Task<CustomerDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetById(id, cancellationToken);
        }

        public async Task Update(CustomerDtoModel entity, CancellationToken cancellationToken)
        {
            await _customerRepository.Update(entity, cancellationToken);
        }

        public async Task<int> FindCurrentCustomerId(CancellationToken cancellationToken)
        {
            return await _customerRepository.FindCurrentCustomerId(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _customerRepository.Delete(id, cancellationToken);
        }


    }
}
