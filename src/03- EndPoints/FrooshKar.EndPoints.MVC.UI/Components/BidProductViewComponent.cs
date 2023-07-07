using System.Runtime.InteropServices.ComTypes;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Components
{
	public class BidProductViewComponent : ViewComponent
	{

		private readonly IBidProductAppService _bidProductAppService;


		public BidProductViewComponent(IBidProductAppService bidProductAppService)
		{
			_bidProductAppService = bidProductAppService;
		}

		public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
		{
			var getAllBidProducts = await _bidProductAppService.GetAll(cancellationToken);
			var notDeletedBidProducts = getAllBidProducts.Where(x => x.IsDeleted == false).ToList();
			var startedBidProducts =
				notDeletedBidProducts.Where(x => x.StartBidTime <= DateTime.Now && x.EndBidTime >= DateTime.Now)
					.ToList();
			return View(startedBidProducts);
		}
	}
}