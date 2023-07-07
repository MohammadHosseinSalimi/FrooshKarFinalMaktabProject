using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.AppService.AppServices
{


	public class BidAppService : IBidAppService
	{

		private readonly IBidService _bidService;

		public BidAppService(IBidService bidService)
		{
			_bidService = bidService;
		}
		public async Task Create(BidDtoModel entity, CancellationToken cancellationToken)
		{
			await _bidService.Create(entity, cancellationToken);
		}

		public async Task<List<BidDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _bidService.GetAll(cancellationToken);
		}

		public async Task<BidDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _bidService.GetById(id, cancellationToken);
		}

		public async Task Update(BidDtoModel entity, CancellationToken cancellationToken)
		{
			await _bidService.Update(entity, cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{
			await _bidService.Delete(id, cancellationToken);
		}



	}
}
