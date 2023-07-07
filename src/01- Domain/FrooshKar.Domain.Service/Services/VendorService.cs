using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.Domain.Service.Services
{
	public class VendorService : IVendorService
	{
		private readonly IVendorRepository _vendorRepository;

		public VendorService(IVendorRepository vendorRepository)
		{
			_vendorRepository = vendorRepository;
		}

		public async Task Create(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			await _vendorRepository.Create(entity, cancellationToken);
		}

		public async Task<List<VendorDtoModel>> GetAll(CancellationToken cancellationToken)
		{
			return await _vendorRepository.GetAll(cancellationToken);
		}

		public async Task<VendorDtoModel> GetById(int id, CancellationToken cancellationToken)
		{
			return await _vendorRepository.GetById(id, cancellationToken);
		}

		public async Task Update(VendorDtoModel entity, CancellationToken cancellationToken)
		{
			await _vendorRepository.Update(entity, cancellationToken);
		}

		public async Task Delete(int id, CancellationToken cancellationToken)
		{
			await _vendorRepository.Delete(id, cancellationToken);
		}

		public async Task<int> FindCurrentVendorId(CancellationToken cancellationToken)
		{
			return await _vendorRepository.FindCurrentVendorId(cancellationToken);
		}



		public async Task<double> VendorTotalWage(int id, double? vendorWagePercent, CancellationToken cancellationToken)
		{
			//todo: edit this
			double fixedPriceWage = 0;
			double bidWage = 0;
			var vendorDtoFromBidProduct = await _vendorRepository.VendorWagePercentFromBidProduct(id, cancellationToken);
			var vendorDtoFromFixedPriceProduct =
				await _vendorRepository.VendorWagePercentFromFixedPriceProduct(id, cancellationToken);
			//todo: correct this code

			foreach (var item in vendorDtoFromBidProduct.BidProducts)
			{
				if (!item.HasNoRecommend && item.FinalBidPrice!=null)
				{
					bidWage = (double)(vendorWagePercent * item.FinalBidPrice) + bidWage;
				}
			}
			foreach (var item in vendorDtoFromFixedPriceProduct.FixedPriceProducts)
			{

				foreach (var member in item.Carts)
				{
					if (member.IsFinished.Value)
					{
						fixedPriceWage = (double)(member.Count * member.FixedPriceProduct.UnitPrice * vendorWagePercent) +
										 fixedPriceWage;
					}




				}

			}



			double totalWage = fixedPriceWage + bidWage;


			return totalWage;

		}

		public async Task CreateByAppUser(VendorDtoModel entity, int appUserId, CancellationToken cancellationToken)
		{
			await _vendorRepository.CreateByAppUser(entity, appUserId, cancellationToken);
		}


		public async Task<List<VendorDtoModel>> GetAllWithProducts(CancellationToken cancellationToken)
		{
			return await _vendorRepository.GetAllWithProducts(cancellationToken);
		}



	}
}
