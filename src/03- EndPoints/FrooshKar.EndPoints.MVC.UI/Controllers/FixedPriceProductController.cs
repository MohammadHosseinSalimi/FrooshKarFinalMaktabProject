using System.Runtime.CompilerServices;
using System.Threading;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
	public class FixedPriceProductController : Controller
	{

		private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
		private readonly ICustomerAppService _customerAppService;
		private readonly ICartAppService _cartAppService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;


		public FixedPriceProductController(IFixedPriceProductAppService fixedPriceProductAppService, ICustomerAppService customerAppService, ICartAppService cartAppService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_fixedPriceProductAppService = fixedPriceProductAppService;
			_customerAppService = customerAppService;
			_cartAppService = cartAppService;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}


		public async Task<IActionResult> ShowAllFixedPriceProduct(CancellationToken cancellationToken)
		{
			if (_contextAccessor.HttpContext.User.Identity.Name == null)
			{
				ViewData["currentCustomerId"] = 0;

			}

			else
			{
				var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
				var rolesAsync = await _userManager.GetRolesAsync(currentUser);
				if (rolesAsync.FirstOrDefault() == "Customer")
				{
					var currentCustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken);
					ViewData["currentCustomerId"] = currentCustomerId;
				}
				else
				{
					ViewData["currentCustomerId"] = 0;
				}
			}

			var getAllFixedPriceProducts = await _fixedPriceProductAppService.GetAll(cancellationToken);
			var notDeletedFixedPriceProducts = getAllFixedPriceProducts.Where(x => x.IsDeleted == false).ToList();
			return View("ShowFixedPriceProducts", notDeletedFixedPriceProducts);
		}

		public async Task<IActionResult> ShowAllFixedPriceProductByCategory(int categoryId, CancellationToken cancellationToken)
		{
			if (_contextAccessor.HttpContext.User.Identity.Name == null)
			{
				ViewData["currentCustomerId"] = 0;

			}

			else
			{
				var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
				var rolesAsync = await _userManager.GetRolesAsync(currentUser);
				if (rolesAsync.FirstOrDefault() == "Customer")
				{
					var currentCustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken);
					ViewData["currentCustomerId"] = currentCustomerId;
				}
				else
				{
					ViewData["currentCustomerId"] = 0;
				}
			}

			var getAllFixedPriceProducts = await _fixedPriceProductAppService.GetAll(cancellationToken);
			var notDeletedFixedPriceProducts = getAllFixedPriceProducts.Where(x => x.IsDeleted == false && x.CategoryId == categoryId).ToList();
			return View("ShowFixedPriceProducts", notDeletedFixedPriceProducts);
		}


		public async Task<IActionResult> ShowAllFixedPriceProductByVendor(int vendorId, CancellationToken cancellationToken)
		{
			if (_contextAccessor.HttpContext.User.Identity.Name == null)
			{
				ViewData["currentCustomerId"] = 0;

			}

			else
			{
				var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
				var rolesAsync = await _userManager.GetRolesAsync(currentUser);
				if (rolesAsync.FirstOrDefault() == "Customer")
				{
					var currentCustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken);
					ViewData["currentCustomerId"] = currentCustomerId;
				}
				else
				{
					ViewData["currentCustomerId"] = 0;
				}
			}


			var getAllFixedPriceProducts = await _fixedPriceProductAppService.GetAll(cancellationToken);
			var notDeletedFixedPriceProducts = getAllFixedPriceProducts.Where(x => x.IsDeleted == false && x.VendorId == vendorId).ToList();
			return View("ShowFixedPriceProducts", notDeletedFixedPriceProducts);
		}


	}
}
