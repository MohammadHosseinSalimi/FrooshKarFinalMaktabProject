using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
    public class FactorService : IFactorService
    {
        private readonly IFactorRepository _factorRepository;

        public FactorService(IFactorRepository factorRepository)
        {
            _factorRepository = factorRepository;
        }
        public async Task Create(FactorDtoModel entity, CancellationToken cancellationToken)
        {
            await _factorRepository.Create(entity, cancellationToken);
        }

        public async Task<List<FactorDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _factorRepository.GetAll(cancellationToken);
        }

        public async Task<FactorDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _factorRepository.GetById(id, cancellationToken);
        }

        public async Task Update(FactorDtoModel entity, CancellationToken cancellationToken)
        {
            await _factorRepository.Update(entity, cancellationToken);
        }

        public async Task<List<FactorDtoModel>> GetAllWithVendor(CancellationToken cancellationToken)
        {
	        return await _factorRepository.GetAllWithVendor(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _factorRepository.Delete(id, cancellationToken);
        }


    }
}
