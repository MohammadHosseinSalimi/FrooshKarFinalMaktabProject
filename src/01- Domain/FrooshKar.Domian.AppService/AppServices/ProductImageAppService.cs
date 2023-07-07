using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class ProductImageAppService : IProductImageAppService
    {

        private readonly IProductImageService _productImageService;

        public ProductImageAppService(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        public async Task Create(ProductImageDtoModel entity, CancellationToken cancellationToken)
        {
            await _productImageService.Create(entity, cancellationToken);
        }

        public async Task<List<ProductImageDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _productImageService.GetAll(cancellationToken);
        }

        public async Task<ProductImageDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _productImageService.GetById(id, cancellationToken);
        }

        public async Task Update(ProductImageDtoModel entity, CancellationToken cancellationToken)
        {
            await _productImageService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _productImageService.Delete(id, cancellationToken);
        }





    }
}
