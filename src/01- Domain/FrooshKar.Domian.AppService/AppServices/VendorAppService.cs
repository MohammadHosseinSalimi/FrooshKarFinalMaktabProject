using System.Runtime.CompilerServices;
using System.Threading;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.AppService.AppServices
{
	public class VendorAppService : IVendorAppService
	{
		private readonly IVendorService _vendorService;
		private readonly IMedalService _medalService;
		private readonly ICartAppService _cartAppService;
		private readonly IBidProductService _bidProductService;

		public VendorAppService(IVendorService vendorService, IMedalService medalService, ICartAppService cartAppService, IBidProductService bidProductService)
		{
			_vendorService = vendorService;
			_medalService = medalService;
			_cartAppService = cartAppService;
			_bidProductService = bidProductService;
		}

		public async Task Create(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			var getAllMedals = await _medalService.GetAll(cancellationToken);
			var SortedMedalBySellLimit = getAllMedals.OrderBy(x => x.SellLimit).ToList();

			entity.MedalId = SortedMedalBySellLimit.FirstOrDefault().Id;

			await _vendorService.Create(entity, cancellationToken);
		}

		public async Task<List<VendorDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _vendorService.GetAll(cancellationToken);
		}

		public async Task<VendorDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _vendorService.GetById(id, cancellationToken);
		}

		public async Task Update(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			await _vendorService.Update(entity, cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{
			await _vendorService.Delete(id, cancellationToken);
		}

		public async Task<int> FindCurrentVendorId(CancellationToken cancellationToken)
		{
			return await _vendorService.FindCurrentVendorId(cancellationToken);
		}


		public async Task<double> FindVendorWageForEachFactor(int vendorId,int factorId, CancellationToken cancellationToken)
		{
			double price = 0;
			var vendorDtoModel = await GetById(vendorId, cancellationToken);

			var medalDtoModel = await _medalService.GetById(vendorDtoModel.Id, cancellationToken);

			var carts = await _cartAppService.GetAll(cancellationToken);
			var cartDtoModels = carts.Where(x => x.FactorId == factorId).ToList();
			foreach (var item in cartDtoModels)
			{
				price=(double)((item.FixedPriceProduct.UnitPrice * item.Count)*medalDtoModel.WagePercent);
			}

			return price;

		}



		public async Task<double> FindVendorWageForEachBidProduct(int vendorId, int bidProductId,
			CancellationToken cancellationToken)
		{
			double price = 0;
			var vendorDtoModel = await GetById(vendorId, cancellationToken);
			var medalDtoModel = await _medalService.GetById(vendorDtoModel.Id, cancellationToken);
			var productDtoModel = await _bidProductService.GetById(bidProductId, cancellationToken);

			price = (double)((productDtoModel.FinalBidPrice) * medalDtoModel.WagePercent);
			return price;

		}




		public async Task UpdateVendorMedal(int id, CancellationToken cancellationToken)
		{
			var vendorMedalId = 0;
			var entity = await _vendorService.GetById(id, cancellationToken);
			var vendorTotalSell = await _vendorService.VendorTotalWage(id, 1, cancellationToken);

			var getAllMedals = await _medalService.GetAll(cancellationToken);
			var SortedMedalBySellLimit = getAllMedals.OrderBy(x => x.SellLimit).ToList();

			for (int i = 0; i < SortedMedalBySellLimit.Count; i++)
			{
				if (vendorTotalSell < SortedMedalBySellLimit[i].SellLimit)
				{
					vendorMedalId = SortedMedalBySellLimit[i].Id;
					break;
				}
			}

			entity.MedalId = vendorMedalId;
			await _vendorService.Update(entity, cancellationToken);
		}


		public async Task CreateByAppUser(VendorDtoModel entity, int appUserId, CancellationToken cancellationToken)
		{
			var getAllMedals = await _medalService.GetAll(cancellationToken);
			var SortedMedalBySellLimit = getAllMedals.OrderBy(x => x.SellLimit).ToList();
			entity.MedalId = SortedMedalBySellLimit[0].Id;

			await _vendorService.CreateByAppUser(entity, appUserId, cancellationToken);
		}

		public async Task<List<VendorDtoModel>> GetAllWithProducts(CancellationToken cancellationToken)
		{
			return await _vendorService.GetAllWithProducts(cancellationToken);
		}
	}
}
