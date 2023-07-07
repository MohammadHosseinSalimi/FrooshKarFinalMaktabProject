using System.Configuration;
using System.Runtime.CompilerServices;
using FrooshKar.Domain.AppService.AppServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.EndPoints.MVC.UI.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using ConfigurationSection = Microsoft.Extensions.Configuration.ConfigurationSection;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{


	[Authorize(Roles = "Vendor")]
	public class VendorController : Controller
	{

		private readonly IVendorAppService _vendorAppService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly ICategoryAppService _categoryAppService;
		private readonly ICityAppService _cityAppService;
		private readonly IAppUserAppService _userAppService;
		private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
		private readonly IBidProductAppService _bidProductAppService;
		private readonly ICommentAppService _commentAppService;
		private readonly PicConfigs _picConfigs;

		public VendorController(IVendorAppService vendorAppService, IWebHostEnvironment webHostEnvironment, ICategoryAppService categoryAppService, ICityAppService cityAppService, IAppUserAppService userAppService, IFixedPriceProductAppService fixedPriceProductAppService, IBidProductAppService bidProductAppService, PicConfigs picConfigs, ICommentAppService commentAppService)
		{
			_vendorAppService = vendorAppService;
			_webHostEnvironment = webHostEnvironment;
			_categoryAppService = categoryAppService;
			_cityAppService = cityAppService;
			_userAppService = userAppService;
			_fixedPriceProductAppService = fixedPriceProductAppService;
			_bidProductAppService = bidProductAppService;
			_picConfigs = picConfigs;
			_commentAppService = commentAppService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetFixedPriceProducts(CancellationToken cancellationToken)
		{
			var id = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var model = await _fixedPriceProductAppService.GetAllByVendorId(id, cancellationToken);
			return View(model);
		}

		public async Task<IActionResult> GetBidProducts(CancellationToken cancellationToken)
		{
			var id = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var model = await _bidProductAppService.GetAllByVendorId(id, cancellationToken);
			return View(model);
		}


		[HttpGet]
		public async Task<IActionResult> CreateFixedPriceProduct(CancellationToken cancellationToken)
		{
			var model = new FixedPriceProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken)
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> CreateFixedPriceProduct(FixedPriceProductViewModel model,
			CancellationToken cancellationToken)

		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.FixedPriceProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);

			int findCurrentVendorId = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var currentVendor = await _vendorAppService.GetById(findCurrentVendorId, cancellationToken);


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
			};

			currentVendor.FixedPriceProducts.Add(fixedPriceProduct);

			await _vendorAppService.Update(currentVendor, cancellationToken);
			return RedirectToAction("index");
		}


		[HttpGet]
		public async Task<IActionResult> CreateBidProduct(CancellationToken cancellationToken)
		{
			var model = new BidProductViewModel()
			{
				Categories = await _categoryAppService.GetAll(cancellationToken)
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> CreateBidProduct(BidProductViewModel model,
			CancellationToken cancellationToken)

		{

			var productImageNames = await AddImage.AddMultipleImage(_picConfigs.BidProductImageFolderName, model.FormFiles,
				_webHostEnvironment.WebRootPath, cancellationToken);
			int findCurrentVendorId = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var currentVendor = await _vendorAppService.GetById(findCurrentVendorId, cancellationToken);

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
				StartBidTime = model.StartBidTime,
				EndBidTime = model.EndBidTime,
				Description = model.Description,
				BasePrice = model.BasePrice,
				ProductImages = productImages,
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



		public async Task<IActionResult> UpdateVendorProfile(CancellationToken cancellationToken)
		{
			var id = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var record = await _vendorAppService.GetById(id, cancellationToken);
			//var cityDtoModels = await _cityAppService.GetById(record.CityId, cancellationToken);
			var cities = await _cityAppService.GetAll(cancellationToken);
			var appUserDtoModel = await _userAppService.GetById((int)record.AppUserId, cancellationToken);

			ViewBag.Id = id;


			var model = new VendorViewModel()
			{
				Address = record.Address,
				AppUserId = record.AppUserId,
				BirthDate = record.BirthDate,
				//City = cityDtoModels.Title,
				Email = appUserDtoModel.Email,
				FirstName = record.FirstName,
				LastName = record.LastName,
				Gender = record.Gender,
				Id = record.Id,
				Password = appUserDtoModel.Password,
				UserName = appUserDtoModel.UserName,
				Role = appUserDtoModel.Role,
				Cities = cities
			};


			model.PersianCalender = model.BirthDate.ToPersianCalenderWithoutHour();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateVendorProfile(VendorViewModel model, CancellationToken cancellationToken)
		{
			var id = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var record = await _vendorAppService.GetById(id, cancellationToken);
			var vendorImageName = await AddImage.AddSingleImage(_picConfigs.ProfileImageFolderName, model.ImageFile,
				_webHostEnvironment.WebRootPath, cancellationToken);
			model.ProfileImageName = vendorImageName;
			model.ProfileImageUrl = "/" + _picConfigs.FixedPriceProductImageFolderName + "/" + vendorImageName;



			AppUserDtoModel appUserDto = new AppUserDtoModel()
			{
				Id = (int)model.AppUserId,
				Email = model.Email,
				Password = model.Password,
				UserName = model.UserName,
				Role = "Vendor"
			};


			await _userAppService.UpdateUser(appUserDto, cancellationToken);
			VendorDtoModel vendorDto = new VendorDtoModel()
			{
				Address = model.Address,
				AppUserId = model.AppUserId,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.GenderEnum.ConvertGenderEnumToString(),
				Id = model.Id,
				CityId = model.CityId,
				MedalId = record.MedalId,
				ProfileImageName = model.ProfileImageName

			};
			await _vendorAppService.Update(vendorDto, cancellationToken);


			return RedirectToAction("Index");
		}


		public async Task<IActionResult> ShowComments(CancellationToken cancellationToken)
		{
			var vendorId = await _vendorAppService.FindCurrentVendorId(cancellationToken);
			var comments = await _commentAppService.GetAllCommentsWithVendors(cancellationToken);
			var model = new List<CommentDtoModel>();
			foreach (var item in comments)
			{
				if (item.IsValidByAdmin)
				{
					var comment = item.Factor.Carts.Any(x => x.FixedPriceProduct.Vendor.Id == vendorId);
					if (comment)
						model.Add(item);
				}
			}

			return View(model);
		}

	}


}
