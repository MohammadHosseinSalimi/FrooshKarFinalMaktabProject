using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{
    public class MedalAppService : IMedalAppService
    {

        private readonly IMedalService _medalService;

        public MedalAppService(IMedalService medalService)
        {
            _medalService = medalService;
        }
        public async Task Create(MedalDtoModel entity, CancellationToken cancellationToken)
        {
            await _medalService.Create(entity, cancellationToken);
        }

        public async Task<List<MedalDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _medalService.GetAll(cancellationToken);
        }

        public async Task<MedalDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _medalService.GetById(id, cancellationToken);
        }

        public async Task Update(MedalDtoModel entity, CancellationToken cancellationToken)
        {
            await _medalService.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _medalService.Delete(id, cancellationToken);
        }






    }
}
