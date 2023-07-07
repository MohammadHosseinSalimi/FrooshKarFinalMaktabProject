using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class FactorAppService : IFactorAppService
    {
        private readonly IFactorService _factorService;

        public FactorAppService(IFactorService factorService)
        {
            _factorService = factorService;
        }
        public async Task Create(FactorDtoModel entity, CancellationToken cancellationToken)
        {
            await _factorService.Create(entity, cancellationToken);
        }

        public async Task<List<FactorDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _factorService.GetAll(cancellationToken);
        }

        public async Task<FactorDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _factorService.GetById(id, cancellationToken);
        }

        public async Task Update(FactorDtoModel entity, CancellationToken cancellationToken)
        {
            await _factorService.Update(entity, cancellationToken);
        }
        public async Task<List<FactorDtoModel>> FindFactorWageByVendor(int vendorId,CancellationToken cancellationToken)
        {

	        var record=await _factorService.GetAllWithVendor(cancellationToken);
            var filteredRecord = record.Where(x => x.Carts.Any(i => i.FixedPriceProduct?.Vendor?.Id == vendorId)).ToList();
			return filteredRecord;

        }
		public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _factorService.Delete(id, cancellationToken);
        }


    }
}
