using System.Threading;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
	[Authorize(Roles = "Customer")]
	public class BidProductController : Controller
	{
		private readonly IBidProductAppService _bidProductAppService;

		public BidProductController(IBidProductAppService bidProductAppService)
		{
			_bidProductAppService = bidProductAppService;
		}



		public async Task<IActionResult> AddCustomerPriceToProduct(int id, CancellationToken cancellationToken)
		{
			ViewBag.Id = id;
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> AddCustomerPriceToProduct(int id, int? price, CancellationToken cancellationToken)
		{

			bool result = await _bidProductAppService.AddCustomerBidToProduct(id, price, cancellationToken);
			if (result == false)
				ModelState.AddModelError(string.Empty, "قیمت وارد شده پایین تر از بالاترین قیمت یا قیمت مبنا است");
			return RedirectToAction("Index", "Home");
		}
	}
}
