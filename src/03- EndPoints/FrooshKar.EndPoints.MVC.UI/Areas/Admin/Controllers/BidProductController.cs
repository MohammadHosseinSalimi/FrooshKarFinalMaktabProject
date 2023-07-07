using System.Data;
using FrooshKar.Domain.AppService.AppServices;
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
    public class BidProductController : Controller
	{
		private readonly ICategoryAppService _categoryAppService;
		private readonly PicConfigs _picConfigs;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IVendorAppService _vendorAppService;
		private readonly IBidProductAppService _bidProductAppService;
		public BidProductController(ICategoryAppService categoryAppService, PicConfigs picConfigs, IWebHostEnvironment webHostEnvironment, IVendorAppService vendorAppService, IBidProductAppService bidProductAppService)
		{
			_categoryAppService = categoryAppService;
			_picConfigs = picConfigs;
			_webHostEnvironment = webHostEnvironment;
			_vendorAppService = vendorAppService;
			_bidProductAppService = bidProductAppService;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			return View(await _bidProductAppService.GetAll(cancellationToken));
		}

		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			await _bidProductAppService.Delete(id, cancellationToken);
			return RedirectToAction("Index");
		}



		[HttpGet]
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			var model = new BidProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken),
				Vendors = await _vendorAppService.GetAll(cancellationToken)
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Create(BidProductViewModel model,
			CancellationToken cancellationToken)
		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.BidProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);

			var currentVendor = await _vendorAppService.GetById(model.VendorId, cancellationToken);

			var productImages = new List<ProductImage>();

			foreach (var item in productImageNames)
			{
				productImages.Add(new ProductImage()
				{
					Title = item,
					Url = "/" + _picConfigs.BidProductImageFolderName + "/" + item
				});
			}


			var bidProduct = new BidProduct()
			{
				Title = model.Title,
				CategoryId = model.CategoryId,
				StartBidTime = model.StartBidTimePersianCalender.ToEnglishCalender(),
				EndBidTime = model.EndBidTimePersianCalender.ToEnglishCalender(),
				Description = model.Description,
				BasePrice = model.BasePrice,
				ProductImages = productImages,
				VendorId = model.VendorId,
			};

			currentVendor.BidProducts.Add(bidProduct);

			await _vendorAppService.Update(currentVendor, cancellationToken);

			var bidProductDto = new BidProductDtoModel()
			{
				Title = model.Title,
				CategoryId = model.CategoryId,
				StartBidTime = model.StartBidTimePersianCalender.ToEnglishCalender(),
				EndBidTime = model.EndBidTimePersianCalender.ToEnglishCalender(),
				Description = model.Description,
				BasePrice = model.BasePrice,
				ProductImages = productImages,
				VendorId = model.VendorId,
			};


			await _bidProductAppService.OpenBidProduct(bidProductDto, cancellationToken);
			await _bidProductAppService.SellProductWithDelay(bidProductDto, cancellationToken);

			return RedirectToAction("index");
		}


		[HttpGet]
		public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
		{
			var record = await _bidProductAppService.GetById(id, cancellationToken);

			var model = new BidProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken),
				VendorId = (int)record.VendorId,
				Id = record.Id,
				Title=record.Title,
				StartBidTime = record.StartBidTime,
				EndBidTime = record.EndBidTime,
				Description = record.Description,
				BasePrice = record.BasePrice
			};

			model.StartBidTimePersianCalender = model.StartBidTime.ToPersianCalenderWithoutHour();
			model.EndBidTimePersianCalender = model.EndBidTime.ToPersianCalenderWithoutHour();

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Update(BidProductViewModel model,
			CancellationToken cancellationToken)
		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.BidProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);


			var productImages = new List<ProductImage>();

			foreach (var item in productImageNames)
			{
				productImages.Add(new ProductImage()
				{
					Title = item,
					Url = "/" + _picConfigs.BidProductImageFolderName + "/" + item
				});
			}

			var product = await _bidProductAppService.GetById(model.Id, cancellationToken);


			var bidProductDto = new BidProductDtoModel()
			{
				Id=model.Id,
				Title = model.Title,
				CategoryId = model.CategoryId,
				StartBidTime = model.StartBidTimePersianCalender.ToEnglishCalender(),
				EndBidTime = model.EndBidTimePersianCalender.ToEnglishCalender(),
				Description = model.Description,
				BasePrice = model.BasePrice,
				ProductImages = productImages,
				VendorId = model.VendorId,
			};


			await _bidProductAppService.Update(bidProductDto, cancellationToken);
			return RedirectToAction("index");
		}

	}
}
