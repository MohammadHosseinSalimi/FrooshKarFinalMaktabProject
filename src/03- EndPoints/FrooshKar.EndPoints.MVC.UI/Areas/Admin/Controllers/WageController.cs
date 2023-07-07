using System.Data;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class WageController : Controller
	{
		private readonly IBidProductAppService _bidProductAppService;
		private readonly IVendorAppService _vendorAppService;
		private readonly IFactorAppService _factorAppService;

		public WageController(IBidProductAppService bidProductAppService, IVendorAppService vendorAppService, IFactorAppService factorAppService)
		{
			_bidProductAppService = bidProductAppService;
			_vendorAppService = vendorAppService;
			_factorAppService = factorAppService;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var vendors = await _vendorAppService.GetAllWithProducts(cancellationToken);

			
			var wageViewModelList = new List<WageViewModel>();
			foreach (var item in vendors)
			{
                double wageFromBidPrice = 0;
                double wageFromFixedPrice = 0;
                var findBidProductWageByVendor = item.BidProducts;
				foreach (var member1 in findBidProductWageByVendor)
				{
					if (member1.Wage != null)
						wageFromBidPrice += (double)member1.Wage;
				}

				var findFactorWageByVendor = await _factorAppService.FindFactorWageByVendor(item.Id, cancellationToken);
				foreach (var member2 in findFactorWageByVendor)
				{
					if (member2.Wage != null)
						wageFromFixedPrice += (double)member2.Wage;
				}

				wageViewModelList.Add(new WageViewModel()
				{
					Id=item.Id,
					VendorFirstName = item.FirstName,
					VendorLastName = item.LastName,
					BidWage = wageFromBidPrice,
					FixedPriceWage = wageFromFixedPrice,
					TotalWage = wageFromFixedPrice+wageFromBidPrice
				});



			}


			return View(wageViewModelList);
		}
	}
}
