using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class FixedPriceProductService : IFixedPriceProductService
    {
        private readonly IFixedPriceProductRepository _fixedPriceProductRepository;

        public FixedPriceProductService(IFixedPriceProductRepository fixedPriceProductRepository)
        {
            _fixedPriceProductRepository = fixedPriceProductRepository;
        }
        public async Task Create(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductRepository.Create(entity, cancellationToken);
        }

        public async Task<List<FixedPriceProductDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _fixedPriceProductRepository.GetAll(cancellationToken);
        }

        public async Task<FixedPriceProductDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _fixedPriceProductRepository.GetById(id, cancellationToken);
        }

        public async Task Update(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _fixedPriceProductRepository.Delete(id, cancellationToken);
        }

        public async Task ValidationByAdmin(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductRepository.ValidationByAdmin(entity, cancellationToken);

        }

        public async Task<List<FixedPriceProductDtoModel>> GetAllByVendorId(int id, CancellationToken cancellationToken)
        {
            return await _fixedPriceProductRepository.GetAllbyVendorId(id, cancellationToken);
        }
    }
}
