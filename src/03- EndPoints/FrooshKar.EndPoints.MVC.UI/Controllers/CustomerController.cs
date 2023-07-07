using FrooshKar.Domain.AppService.AppServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
	[Authorize(Roles = "Customer")]
	public class CustomerController : Controller
	{
		private readonly ICustomerAppService _customerAppService;
		private readonly IFixedPriceProductAppService _fixedPriceProductAppService;
		private readonly ICartAppService _cartAppService;
		private readonly ICityAppService _cityAppService;
		private readonly IAppUserAppService _userAppService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly PicConfigs _picConfigs;

		public CustomerController(ICustomerAppService customerAppService, IFixedPriceProductAppService fixedPriceProductAppService, ICartAppService cartAppService, ICityAppService cityAppService, IAppUserAppService userAppService, IWebHostEnvironment webHostEnvironment, PicConfigs picConfigs)
		{
			_customerAppService = customerAppService;
			_fixedPriceProductAppService = fixedPriceProductAppService;
			_cartAppService = cartAppService;
			_cityAppService = cityAppService;
			_userAppService = userAppService;
			_webHostEnvironment = webHostEnvironment;
			_picConfigs = picConfigs;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> AddProductToCustomerCart(int id, CancellationToken cancellationToken)
		{
			//todo: we cant add a product from different vendors to a customer cart
			var findProduct = await _fixedPriceProductAppService.GetById(id, cancellationToken);

			var productCart = new Cart()
			{
				Count = 1,
				CustomerId = await _customerAppService.FindCurrentCustomerId(cancellationToken),
				IsFinished = false
			};

			findProduct.Carts.Add(productCart);

			await _fixedPriceProductAppService.Update(findProduct, cancellationToken);

			return RedirectToAction("ShowAllFixedPriceProduct", "FixedPriceProduct");

		}

		public async Task<IActionResult> Update(CancellationToken cancellationToken)
		{
			//todo: use appservice for customer and user 
			int id = await _customerAppService.FindCurrentCustomerId(cancellationToken);
			var record = await _customerAppService.GetById(id, cancellationToken);
			//var cityDtoModels = await _cityAppService.GetById(record.CityId, cancellationToken);
			var cities = await _cityAppService.GetAll(cancellationToken);
			var appUserDtoModel = await _userAppService.GetById((int)record.AppUserId, cancellationToken);

			var model = new CustomerViewModel()
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
		public async Task<IActionResult> Update(CustomerViewModel model, CancellationToken cancellationToken)
		{
			var profileImageName = await AddImage.AddSingleImage(_picConfigs.ProfileImageFolderName, model.ImageFile,
				_webHostEnvironment.WebRootPath, cancellationToken);
			model.ProfileImageName = profileImageName;
			model.ProfileImageUrl = "/" + _picConfigs.ProfileImageFolderName + "/" + profileImageName;





			AppUserDtoModel appUserDto = new AppUserDtoModel()
			{
				Id = (int)model.AppUserId,
				Email = model.Email,
				Password = model.Password,
				UserName = model.UserName,
				Role = "Customer"
			};


			await _userAppService.UpdateUser(appUserDto, cancellationToken);
			CustomerDtoModel customerDto = new CustomerDtoModel()
			{
				Address = model.Address,
				AppUserId = model.AppUserId,
				BirthDate = model.PersianCalender.ToEnglishCalender(),
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.GenderEnum.ConvertGenderEnumToString(),
				Id = model.Id,
				CityId = model.CityId,
				ProfileImageName = model.ProfileImageName

			};
			await _customerAppService.Update(customerDto, cancellationToken);


			return RedirectToAction("Index");
		}


	}
}
