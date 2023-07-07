using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Create(CartDtoModel entity, CancellationToken cancellationToken)
        {
            await _cartRepository.Create(entity, cancellationToken);
        }

        public async Task<List<CartDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _cartRepository.GetAll(cancellationToken);
        }

        public async Task<CartDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _cartRepository.GetById(id, cancellationToken);
        }

        public async Task Update(CartDtoModel entity, CancellationToken cancellationToken)
        {
            await _cartRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _cartRepository.Delete(id, cancellationToken);
        }


    }
}
