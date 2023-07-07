using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
	[Authorize(Roles = "Customer")]
	public class FactorController : Controller
	{
		private readonly ICustomerAppService _customerAppService;
		private readonly IFactorAppService _factorAppService;
		private readonly ICommentAppService _commentAppService;

		public FactorController(ICustomerAppService customerAppService, IFactorAppService factorAppService, ICommentAppService commentAppService)
		{
			_customerAppService = customerAppService;
			_factorAppService = factorAppService;
			_commentAppService = commentAppService;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var currentCustomer = await _customerAppService.FindCurrentCustomerId(cancellationToken);
			var factors = await _factorAppService.GetAll(cancellationToken);
			var customerFactors = factors.Where(x => x.Carts.Any(c => c.CustomerId == currentCustomer) && x.IsDeleted == false).ToList();
			return View(customerFactors);
		}


		public async Task<IActionResult> ReleaseComment(int factorId, CancellationToken cancellationToken)
		{
			var model = new CommentDtoModel()
			{
				FactorId = factorId
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> ReleaseComment(CommentDtoModel model, CancellationToken cancellationToken)
		{
			await _commentAppService.Create(model, cancellationToken);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ShowFactorsByDetail(int factorId, CancellationToken cancellationToken)
		{
			var currentCustomer = await _customerAppService.FindCurrentCustomerId(cancellationToken);
			var factors = await _factorAppService.GetAll(cancellationToken);
			var currentCustomerFinishedCarts = factors.Where(x => x.Carts.Any(c => c.CustomerId == currentCustomer) && x.IsDeleted == false).FirstOrDefault(y => y.Id == factorId).Carts;
			return View(currentCustomerFinishedCarts);
		}


	}
}
