using System.Security.Cryptography.X509Certificates;
using System.Threading;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;
using Hangfire;

namespace FrooshKar.Domain.AppService.AppServices
{


	public class BidProductAppService : IBidProductAppService
	{
		private readonly IBidProductService _bidProductService;
		private readonly IBidAppService _bidAppService;
		private readonly ICustomerAppService _customerAppService;
		private readonly IVendorAppService _vendorAppService;
		public BidProductAppService(IBidProductService bidProductService, IBidAppService bidAppService, ICustomerAppService customerAppService, IVendorAppService vendorAppService)
		{
			_bidProductService = bidProductService;
			_bidAppService = bidAppService;
			_customerAppService = customerAppService;
			_vendorAppService = vendorAppService;
		}
		public async Task Create(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			await _bidProductService.Create(entity, cancellationToken);
		}

		public async Task<List<BidProductDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _bidProductService.GetAll(cancellationToken);
		}

		public async Task<BidProductDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _bidProductService.GetById(id, cancellationToken);
		}

		public async Task Update(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			await _bidProductService.Update(entity, cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{
			await _bidProductService.Delete(id, cancellationToken);
		}

		public async Task<List<BidProductDtoModel>> GetAllByVendorId(int id, CancellationToken cancellationToken)
		{
			return await _bidProductService.GetAllbyVendorId(id, cancellationToken);
		}


		public async Task ValidationByAdmin(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			await _bidProductService.ValidationByAdmin(entity, cancellationToken);
		}


		public async Task<bool> AddCustomerBidToProduct(int bidProductId, int? price, CancellationToken cancellationToken)
		{
			var record = await _bidProductService.GetById(bidProductId, cancellationToken);

			if (price < record.LastMaxModifiedPrice || price < record.BasePrice)
				return false;

			record.LastMaxModifiedPrice = price;
			await _bidProductService.Update(record, cancellationToken);

			var bidDtoModel = new BidDtoModel()
			{
				BidPrice = price,
				BidProductId = bidProductId,
				CustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken)
			};

			await _bidAppService.Create(bidDtoModel, cancellationToken);
			return true;

		}



		public async Task SellBidProduct(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			var bidProductDto = await _bidProductService.GetBidProductAndBid(entity.Id, cancellationToken);
			//todo: is opened  be false 
			int maxBidPrice = 0;
			int maxPerson = 0;

			if (bidProductDto.Bids.Count != 0)
			{
				maxBidPrice = bidProductDto.Bids.Max(x => x.BidPrice).Value;
				maxPerson = bidProductDto.Bids.FirstOrDefault(x => x.BidPrice == maxBidPrice).CustomerId;
			}
			else
			{
				bidProductDto.HasNoRecommend = true;
			}

			bidProductDto.FinalBidPrice = maxBidPrice;
			bidProductDto.WinnerCustomer = maxPerson;
			await _bidProductService.Update(bidProductDto, cancellationToken);

			var bidProductDtoWithoutBid = await _bidProductService.GetById(entity.Id, cancellationToken);
			var vendorId = bidProductDtoWithoutBid.Vendor.Id;
			var price = await _vendorAppService.FindVendorWageForEachBidProduct(vendorId, bidProductDtoWithoutBid.Id,
				cancellationToken);
			bidProductDtoWithoutBid.Wage = price;
			await _bidProductService.Update(bidProductDtoWithoutBid, cancellationToken);
			await _vendorAppService.UpdateVendorMedal(vendorId, cancellationToken);

		}


		public async Task OpenBidProduct(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			entity.IsOpened = true;
			await _bidProductService.Update(entity, cancellationToken);
		}


		public async Task SellProductWithDelay(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			TimeSpan difference = (DateTime)entity.EndBidTime - DateTime.Now;
			BackgroundJob.Schedule(() => SellBidProduct(entity, cancellationToken), difference);
		}

		public async Task OpenBidProductWithDelay(BidProductDtoModel entity, CancellationToken cancellationToken)
		{
			TimeSpan difference = (DateTime)entity.StartBidTime - DateTime.Now;
			BackgroundJob.Schedule(() => OpenBidProduct(entity, cancellationToken), difference);
		}


	}
}
