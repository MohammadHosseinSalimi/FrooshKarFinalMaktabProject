using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class MedalService : IMedalService
    {
        private readonly IMedalRepository _medalRepository;

        public MedalService(IMedalRepository medalRepository)
        {
            _medalRepository = medalRepository;
        }

        public async Task Create(MedalDtoModel entity, CancellationToken cancellationToken)
        {
            await _medalRepository.Create(entity, cancellationToken);
        }

        public async Task<List<MedalDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _medalRepository.GetAll(cancellationToken);
        }

        public async Task<MedalDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _medalRepository.GetById(id, cancellationToken);
        }

        public async Task Update(MedalDtoModel entity, CancellationToken cancellationToken)
        {
            await _medalRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _medalRepository.Delete(id, cancellationToken);
        }






    }
}
