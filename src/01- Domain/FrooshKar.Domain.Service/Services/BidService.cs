using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{


    public class BidService : IBidService
    {

        private readonly IBidRepository _bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task Create(BidDtoModel entity, CancellationToken cancellationToken)
        {
            await _bidRepository.Create(entity, cancellationToken);
        }

        public async Task<List<BidDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidRepository.GetAll(cancellationToken);
        }

        public async Task<BidDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _bidRepository.GetById(id, cancellationToken);
        }

        public async Task Update(BidDtoModel entity, CancellationToken cancellationToken)
        {
            await _bidRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidRepository.Delete(id, cancellationToken);
        }


    }
}
