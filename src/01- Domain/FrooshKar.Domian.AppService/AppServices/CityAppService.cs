using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class CityAppService : ICityAppService
    {
        private readonly ICityService _cityService;

        public CityAppService(ICityService cityService)
        {
            _cityService = cityService;
        }
        public async Task Create(CityDtoModel entity, CancellationToken cancellationToken)
        {
            await _cityService.Create(entity, cancellationToken);
        }

        public async Task<List<CityDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _cityService.GetAll(cancellationToken);
        }

        public async Task<CityDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _cityService.GetById(id, cancellationToken);
        }

        public async Task Update(CityDtoModel entity, CancellationToken cancellationToken)
        {
            await _cityService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _cityService.Delete(id, cancellationToken);
        }


    }
}
