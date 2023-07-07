using System.Runtime.CompilerServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class FixedPriceProductAppService : IFixedPriceProductAppService
    {
        private readonly IFixedPriceProductService _fixedPriceProductService;
        public FixedPriceProductAppService(IFixedPriceProductService fixedPriceProductService)
        {
            _fixedPriceProductService = fixedPriceProductService;
        }
        public async Task Create(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductService.Create(entity, cancellationToken);
        }

        public async Task<List<FixedPriceProductDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _fixedPriceProductService.GetAll(cancellationToken);
        }

        public async Task<FixedPriceProductDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _fixedPriceProductService.GetById(id, cancellationToken);
        }

        public async Task Update(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _fixedPriceProductService.Delete(id, cancellationToken);
        }

        public async Task ValidationByAdmin(FixedPriceProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _fixedPriceProductService.ValidationByAdmin(entity, cancellationToken);
        }

        public async Task<List<FixedPriceProductDtoModel>> GetAllByVendorId(int id, CancellationToken cancellationToken)
        {
            return await _fixedPriceProductService.GetAllByVendorId(id, cancellationToken);
        }


    }
}
