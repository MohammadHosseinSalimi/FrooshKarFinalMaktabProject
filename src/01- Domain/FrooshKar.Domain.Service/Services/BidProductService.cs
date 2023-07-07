using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{


    public class BidProductService : IBidProductService
    {
        private readonly IBidProductRepository _bidProductRepository;

        public BidProductService(IBidProductRepository bidProductRepository)
        {
            _bidProductRepository = bidProductRepository;
        }
        public async Task Create(BidProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _bidProductRepository.Create(entity, cancellationToken);
        }

        public async Task<List<BidProductDtoModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidProductRepository.GetAll(cancellationToken);
        }

        public async Task<BidProductDtoModel> GetById(int id, CancellationToken cancellationToken)
        {
            return await _bidProductRepository.GetById(id, cancellationToken);
        }

        public async Task Update(BidProductDtoModel entity, CancellationToken cancellationToken)
        {
            await _bidProductRepository.Update(entity, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidProductRepository.Delete(id, cancellationToken);
        }

        public async Task<List<BidProductDtoModel>> GetAllbyVendorId(int id, CancellationToken cancellationToken)
        {
            return await _bidProductRepository. GetAllbyVendorId(id, cancellationToken);
        }

        public async Task<BidProductDtoModel> GetBidProductAndBid(int id, CancellationToken cancellationToken)
        {
            return await _bidProductRepository.GetBidProductAndBid(id, cancellationToken);
        }



        public async Task ValidationByAdmin(BidProductDtoModel entity, CancellationToken cancellationToken)
        {
	        await _bidProductRepository.ValidationByAdmin(entity, cancellationToken);
        }

	}
}
