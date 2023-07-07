using System.Security.Cryptography.X509Certificates;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly ICityService _cityService;
        private readonly IAppUserService _appUserService;
        private readonly ICartService _cartService;

        public CustomerAppService(ICustomerService customerService, ICityService cityService, IAppUserService appUserService, ICartService cartService)
        {
	        _customerService = customerService;
	        _cityService = cityService;
	        _appUserService = appUserService;
            _cartService = cartService;
        }
        public async Task CreateByAppUser(CustomerDtoModel entity,int appUserId, CancellationToken cancellationToken)
        {

	        await _customerService.CreateByAppUser(entity,appUserId, cancellationToken);
        }

        public async Task<List<CustomerDtoModel>> GetAll(CancellationToken cancellationToken)
        
        
        {

	       return await _customerService.GetAll(cancellationToken);

        }

		public async Task<CustomerDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _customerService.GetById(id, cancellationToken);
        }

        public async Task Update(CustomerDtoModel entity, CancellationToken cancellationToken)
        {
            await _customerService.Update(entity, cancellationToken);
        }

        public async Task<int> FindCurrentCustomerId(CancellationToken cancellationToken)
        {
            return await _customerService.FindCurrentCustomerId(cancellationToken);

        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _customerService.Delete(id, cancellationToken);
        }


        public async Task CreateCart(int CustomerId, CancellationToken cancellationToken)
        {
            var carts =await _cartService.GetAll(cancellationToken);
            bool customerHasCart = carts.Any(x => x.CustomerId == CustomerId);
            if (!carts.Any(x => x.CustomerId == CustomerId) || carts.FirstOrDefault(x => x.CustomerId == CustomerId).IsFinished == true)
            {
                await _cartService.Create(new CartDtoModel()
                {
                    CustomerId = CustomerId
                }, cancellationToken);
            }
        }
    }
}
