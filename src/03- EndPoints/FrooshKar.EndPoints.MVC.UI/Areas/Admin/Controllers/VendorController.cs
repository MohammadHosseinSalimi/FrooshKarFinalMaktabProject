using AutoMapper;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using FrooshKar.Domain.AppService.AppServices;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class VendorController : Controller
	{

		private readonly IVendorAppService _vendorAppService;
		private readonly IAppUserAppService _userAppService;
		private readonly ICityAppService _cityAppService;
		private readonly IMedalAppService _medalAppService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;
		private readonly PicConfigs _picConfigs;
		public VendorController(IVendorAppService vendorAppService, IWebHostEnvironment webHostEnvironment, IMapper mapper, IAppUserAppService userAppService, ICityAppService cityAppService, IMedalAppService medalAppService, PicConfigs picConfigs)
		{
			_vendorAppService = vendorAppService;
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
			_userAppService = userAppService;
			_cityAppService = cityAppService;
			_medalAppService = medalAppService;
			_picConfigs = picConfigs;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var record = await _vendorAppService.GetAll(cancellationToken);
			var vendorViewModel = new List<VendorViewModel>();


			foreach (var item in record)
			{
				var cityDtoModel = new CityDtoModel();
				if (item.CityId != null)
				{
					cityDtoModel = await _cityAppService.GetById((int)item.CityId, cancellationToken);
				}
				var medalDtoModel = await _medalAppService.GetById(item.MedalId, cancellationToken);
				var appUserDtoModel = await _userAppService.GetById((int)item.AppUserId, cancellationToken);

				var model = new VendorViewModel()
				{
					Id = item.Id,
					FirstName = item.FirstName,
					LastName = item.LastName,
					Address = item.Address,
					AppUserId = item.AppUserId,
					BirthDate = item.BirthDate,
					Medal = medalDtoModel.Title,
					Gender = item.Gender,
					IsDeleted = item.IsDeleted,
					ProfileImageName = item.ProfileImageName,
					ProfileImageUrl = item.ProfileImageUrl,
                    Email = appUserDtoModel.Email,
					Password = appUserDtoModel.Password,
					UserName = appUserDtoModel.UserName

				};
				if (item.CityId == null)
				{
					model.City = string.Empty;
				}
				else
				{
					model.City = cityDtoModel.Title;
				}

				vendorViewModel.Add(model);

			}


			return View(vendorViewModel);
		}


		[HttpGet]
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			var cityDtoModels = await _cityAppService.GetAll(cancellationToken);
			var medalDtoModels = await _medalAppService.GetAll(cancellationToken);
			var model = new VendorViewModel()
			{
				Cities = cityDtoModels,
				Medals = medalDtoModels
			};

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Create(VendorViewModel model,
			CancellationToken cancellationToken)
		{
			var appUserDtoModel = new AppUserDtoModel()
			{
				Password = model.Password,
				ConfirmPassword = model.Password,
				Email = model.Email,
				Role = "Vendor",
				UserName = model.Email
			};

			var appUserId = await _userAppService.Create(appUserDtoModel, cancellationToken);


			var profileImageName = await AddImage.AddSingleImage(_picConfigs.ProfileImageFolderName, model.ImageFile,
				_webHostEnvironment.WebRootPath, cancellationToken);
			model.ProfileImageName = profileImageName;
			model.ProfileImageUrl = "/" + _picConfigs.ProfileImageFolderName + "/" + profileImageName;



			var mapping = _mapper.Map<VendorDtoModel>(model);
            
			mapping.BirthDate = model.PersianCalender.ToEnglishCalender();
			mapping.Gender = model.GenderEnum.ConvertGenderEnumToString();

			await _vendorAppService.CreateByAppUser(mapping, appUserId, cancellationToken);
			return RedirectToAction("Index");

		}


		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var vendorDtoModel = await _vendorAppService.GetById(id, cancellationToken);

			await _userAppService.DeleteUser((int)vendorDtoModel.AppUserId, cancellationToken);
			await _vendorAppService.Delete(id, cancellationToken);

			return RedirectToAction("Index");

		}
		public async Task<IActionResult> Update(int id,CancellationToken cancellationToken)
		{
			var record = await _vendorAppService.GetById(id, cancellationToken);
			//var cityDtoModels = await _cityAppService.GetById(record.CityId, cancellationToken);
			var cities = await _cityAppService.GetAll(cancellationToken);
			var medals = await _medalAppService.GetAll(cancellationToken);
			var appUserDtoModel = await _userAppService.GetById((int)record.AppUserId, cancellationToken);

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
				Cities = cities,
				Medals = medals
			};


			if (model.BirthDate != null)
				model.PersianCalender = model.BirthDate.ToPersianCalenderWithoutHour();
			else
				model.PersianCalender = string.Empty;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(VendorViewModel model, CancellationToken cancellationToken)
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
				Role = "Vendor"
			};


			await _userAppService.UpdateUser(appUserDto, cancellationToken);
			VendorDtoModel vendorDto = new VendorDtoModel()
			{
				Address = model.Address,
				AppUserId = model.AppUserId,
				BirthDate = model.PersianCalender.ToEnglishCalender(),
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.GenderEnum.ConvertGenderEnumToString(),
				Id = model.Id,
				CityId = model.CityId,
				MedalId = model.MedalId,
				ProfileImageName = model.ProfileImageName,
				ProfileImageUrl = model.ProfileImageUrl

			};
			await _vendorAppService.Update(vendorDto, cancellationToken);


			return RedirectToAction("Index");
		}




	}
}
