using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class CartAppService : ICartAppService
    {
        private readonly ICartService _cartService;

        public CartAppService(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task Create(CartDtoModel entity, CancellationToken cancellationToken)
        {
            await _cartService.Create(entity, cancellationToken);
        }

        public async Task<List<CartDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _cartService.GetAll(cancellationToken);
        }

        public async Task<CartDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _cartService.GetById(id, cancellationToken);
        }

        public async Task Update(CartDtoModel entity, CancellationToken cancellationToken)
        {
            await _cartService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _cartService.Delete(id, cancellationToken);
        }


        public async Task FinalizeCartAndConvertToFactor(CartDtoModel entity, int currentCustomerId, CancellationToken cancellationToken)
        {
            var GetAllCarts = await _cartService.GetAll(cancellationToken);
            var CurrentUserCarts = GetAllCarts.Where(x => x.CustomerId == currentCustomerId).ToList();
            CurrentUserCarts.ForEach(x => x.IsFinished = true);

        }

    }
}
