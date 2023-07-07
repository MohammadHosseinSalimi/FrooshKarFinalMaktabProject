using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.EndPoints.MVC.UI.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class FixedPriceProductController : Controller
	{
		private readonly ICategoryAppService _categoryAppService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IVendorAppService _vendorAppService;
		private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
		private readonly PicConfigs _picConfigs;

		public FixedPriceProductController(ICategoryAppService categoryAppService, IWebHostEnvironment webHostEnvironment, IVendorAppService vendorAppService, IFixedPriceProductAppService fixedPriceProductAppService, PicConfigs picConfigs)
		{
			_categoryAppService = categoryAppService;
			_webHostEnvironment = webHostEnvironment;
			_vendorAppService = vendorAppService;
			_fixedPriceProductAppService = fixedPriceProductAppService;
			_picConfigs = picConfigs;
		}



		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			return View(await _fixedPriceProductAppService.GetAll(cancellationToken));
		}

		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			await _fixedPriceProductAppService.Delete(id, cancellationToken);
			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			var model = new FixedPriceProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken),
				Vendors = await _vendorAppService.GetAll(cancellationToken)
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Create(FixedPriceProductViewModel model, CancellationToken cancellationToken)
		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.FixedPriceProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);


			var currentVendor = await _vendorAppService.GetById((int)model.VendorId, cancellationToken);


			var productImages = new List<ProductImage>();

			foreach (var item in productImageNames)
			{
				productImages.Add(new ProductImage()
				{
					Title = item,
					Url = "/" + _picConfigs.FixedPriceProductImageFolderName + "/" + item
				});
			}


			var fixedPriceProduct = new FixedPriceProduct()
			{
				Title = model.Title,
				CategoryId = model.CategoryId,
				UnitPrice = model.UnitPrice,
				Quantity = model.Quantity,
				Description = model.Description,
				ProductImages = productImages,
				VendorId = model.VendorId
			};

			currentVendor.FixedPriceProducts.Add(fixedPriceProduct);

			await _vendorAppService.Update(currentVendor, cancellationToken);
			return RedirectToAction("index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id,CancellationToken cancellationToken)
		{
			var record=await _fixedPriceProductAppService.GetById(id, cancellationToken);
			var model = new FixedPriceProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken),
				Id = record.Id,
				Description = record.Description,
				Quantity=record.Quantity,
				UnitPrice = record.UnitPrice,
				Title = record.Title,
				VendorId = record.VendorId
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Update(FixedPriceProductViewModel model, CancellationToken cancellationToken)
		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.FixedPriceProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);



			var productImages = new List<ProductImage>();

			foreach (var item in productImageNames)
			{
				productImages.Add(new ProductImage()
				{
					Title = item,
					Url = "/" + _picConfigs.FixedPriceProductImageFolderName + "/" + item
				});
			}



			var product = await _fixedPriceProductAppService.GetById(model.Id, cancellationToken);



			var fixedPriceProductDto = new FixedPriceProductDtoModel()
			{
				Id=model.Id,
				Title = model.Title,
				CategoryId = model.CategoryId,
				UnitPrice = model.UnitPrice,
				Quantity = model.Quantity,
				Description = model.Description,
				ProductImages = productImages,
				VendorId = model.VendorId
			};
			

			await _fixedPriceProductAppService.Update(fixedPriceProductDto, cancellationToken);
			return RedirectToAction("index");
		}

	}
}
