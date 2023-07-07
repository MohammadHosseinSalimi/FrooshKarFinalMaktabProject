using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }


        public async Task Create(ProductImageDtoModel entity, CancellationToken cancellationToken)
        {
            await _productImageRepository.Create(entity, cancellationToken);
        }

        public async Task<List<ProductImageDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _productImageRepository.GetAll(cancellationToken);
        }

        public async Task<ProductImageDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _productImageRepository.GetById(id, cancellationToken);
        }

        public async Task Update(ProductImageDtoModel entity, CancellationToken cancellationToken)
        {
            await _productImageRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _productImageRepository.Delete(id, cancellationToken);
        }






    }
}
