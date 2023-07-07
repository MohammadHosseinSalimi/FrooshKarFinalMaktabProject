using AutoMapper;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.EndPoints.MVC.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Elfie.Model.Map;
using NuGet.Protocol;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
	[Authorize(Roles = "Customer")]
	public class CartController : Controller
	{
		private readonly ICartAppService _cartAppService;
		private readonly ICustomerAppService _customerAppService;
		private readonly IFactorAppService _factorAppService;
		private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
		private readonly IVendorAppService _vendorAppService;
		private readonly IMapper _mapper;

		public CartController(ICustomerAppService customerAppService, ICartAppService cartAppService, IFactorAppService factorAppService, IMapper mapper, IFixedPriceProductAppService fixedPriceProductAppService, IVendorAppService vendorAppService)
		{
			_customerAppService = customerAppService;
			_cartAppService = cartAppService;
			_factorAppService = factorAppService;
			_mapper = mapper;
			_fixedPriceProductAppService = fixedPriceProductAppService;
			_vendorAppService = vendorAppService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ShowCustomerCarts(CancellationToken cancellationToken)
		{
			var currentCustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken);
			var getAll = await _cartAppService.GetAll(cancellationToken);
			var carts = getAll.Where(x => x.CustomerId == currentCustomerId && x.IsFinished == false && x.IsDeleted == false).ToList();
			var model = new CartListViewModel();
			_mapper.Map(carts, model.CartList);
			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> ShowCustomerCarts(CartListViewModel model, CancellationToken cancellationToken)
		{
			var factorDto = new FactorDtoModel();
			await _factorAppService.Create(factorDto, cancellationToken);
			foreach (var item in model.CartList)
			{
				if (item.FixedPriceProduct.Quantity < item.Count)
				{
					ModelState.AddModelError(String.Empty, $"تعداد کالای {item.FixedPriceProduct.Title} نمی تواند بیشتر از {item.FixedPriceProduct.Quantity} باشد");
					break;
				}
			}
			if (!ModelState.IsValid)
			{
				return RedirectToAction("ShowCustomerCarts");
			}

			var lastFactorId = 0;
            int vendorId=0;
			foreach (var item in model.CartList)
			{



				//var cartDto = await _cartAppService.GetById(item.Id, cancellationToken);
				//cartDto.IsFinished = true;
				//cartDto.FixedPriceProduct.Quantity = item.FixedPriceProduct.Quantity - item.Count;
				//await _cartAppService.Update(cartDto, cancellationToken);
				//factorDto.Carts.Add(_mapper.Map<Cart>(cartDto));



				var productDto = await _fixedPriceProductAppService.GetById((int)item.FixedPriceProductId, cancellationToken);

				productDto.Quantity = item.FixedPriceProduct.Quantity - item.Count;
                vendorId = item.FixedPriceProduct.VendorId.Value;
                await _fixedPriceProductAppService.Update(productDto, cancellationToken);
				item.IsFinished = true;
				item.FixedPriceProduct = null;
				var getAllFactors = await _factorAppService.GetAll(cancellationToken);
				lastFactorId = getAllFactors.LastOrDefault().Id;
				item.FactorId = lastFactorId;
				await _cartAppService.Update(_mapper.Map<CartDtoModel>(item), cancellationToken);


            }


            var price = await _vendorAppService.FindVendorWageForEachFactor(vendorId, lastFactorId, cancellationToken);
			var factor = await _factorAppService.GetById(lastFactorId, cancellationToken);

			factor.Wage = price;

			await _factorAppService.Update(factor, cancellationToken);

			await _vendorAppService.UpdateVendorMedal(vendorId, cancellationToken);


			return RedirectToAction("Index", "Factor");



			//todo: I must write input for createdat,createdby,lastmodifiedat, lastmodifiedby in view


		}

		public async Task<IActionResult> DeleteCart(int id, CancellationToken cancellationToken)
		{
			await _cartAppService.Delete(id, cancellationToken);
			return RedirectToAction("ShowCustomerCarts");
		}



		public async Task<IActionResult> Count(int productId, int? count, CancellationToken cancellationToken)
		{
			//bool IsValid = false;
			//var getProduct =await  _fixedPriceProductAppService.GetById((int)productId, cancellationToken);
			//if (getProduct.Quantity>=count)
			//{
			// IsValid=true;
			//}

			//if (IsValid)
			// return Json(true);
			//else
			// return Json($"تعداد انتخابی کالا نمی تواند از {getProduct.Quantity} بیشتر باشد");

			return null;


		}

		//public async Task<IActionResult> UpdateCustomerCarts


	}
}
