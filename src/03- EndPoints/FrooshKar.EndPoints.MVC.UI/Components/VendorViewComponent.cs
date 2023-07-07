using FrooshKar.Domain.Core.Contracts.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Components
{
	public class VendorViewComponent : ViewComponent
	{
		private readonly IVendorAppService _vendorAppService;

		public VendorViewComponent(IVendorAppService vendorAppService)
		{
			_vendorAppService = vendorAppService;
		}

		public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
		{
			var getAllVendors = await _vendorAppService.GetAll(cancellationToken);
			var nonDeletedVendors = getAllVendors.Where(x => x.IsDeleted == false).ToList();
			return View(nonDeletedVendors);
		}

	}
}
