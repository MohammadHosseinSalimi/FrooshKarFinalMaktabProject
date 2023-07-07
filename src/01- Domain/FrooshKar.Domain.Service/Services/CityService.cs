using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task Create(CityDtoModel entity, CancellationToken cancellationToken)
        {
            await _cityRepository.Create(entity, cancellationToken);
        }

        public async Task<List<CityDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _cityRepository.GetAll(cancellationToken);
        }

        public async Task<CityDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _cityRepository.GetById(id, cancellationToken);
        }

        public async Task Update(CityDtoModel entity, CancellationToken cancellationToken)
        {
            await _cityRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _cityRepository.Delete(id, cancellationToken);
        }


    }
}
