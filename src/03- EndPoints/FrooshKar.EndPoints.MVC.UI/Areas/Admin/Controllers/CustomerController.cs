using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using AutoMapper;
using FrooshKar.Domain.AppService.AppServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Domain.Core.Enums;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {

        private readonly ICustomerAppService _customerAppService;
        private readonly IAppUserAppService _userAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly PicConfigs _picConfigs;
        public CustomerController(ICustomerAppService customerAppService, IWebHostEnvironment webHostEnvironment, IMapper mapper, IAppUserAppService userAppService, ICityAppService cityAppService, PicConfigs picConfigs)
        {
            _customerAppService = customerAppService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _userAppService = userAppService;
            _cityAppService = cityAppService;
            _picConfigs = picConfigs;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var record = await _customerAppService.GetAll(cancellationToken);
            var customerViewModel = new List<CustomerViewModel>();


            foreach (var item in record)
            {
                var cityDtoModel = new CityDtoModel();
                if (item.CityId != null)
                {
                    cityDtoModel = await _cityAppService.GetById((int)item.CityId, cancellationToken);
                }
                var appUserDtoModel = await _userAppService.GetById((int)item.AppUserId, cancellationToken);

                var model = new CustomerViewModel()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    AppUserId = item.AppUserId,
                    BirthDate = item.BirthDate,
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

                customerViewModel.Add(model);

            }


            return View(customerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var cityDtoModels = await _cityAppService.GetAll(cancellationToken);
            var model = new CustomerViewModel()
            {
                Cities = cityDtoModels
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel model,
            CancellationToken cancellationToken)
        {
            var appUserDtoModel = new AppUserDtoModel()
            {
                Password = model.Password,
                ConfirmPassword = model.Password,
                Email = model.Email,
                Role = "Customer",
                UserName = model.Email
            };

            var appUserId = await _userAppService.Create(appUserDtoModel, cancellationToken);


            var profileImageName = await AddImage.AddSingleImage(_picConfigs.ProfileImageFolderName, model.ImageFile,
                _webHostEnvironment.WebRootPath, cancellationToken);
            model.ProfileImageName = profileImageName;
            model.ProfileImageUrl = "/" + _picConfigs.ProfileImageFolderName + "/" + profileImageName;


            var mapping = _mapper.Map<CustomerDtoModel>(model);

            mapping.BirthDate = model.PersianCalender.ToEnglishCalender();
            mapping.Gender = model.GenderEnum.ConvertGenderEnumToString();

            await _customerAppService.CreateByAppUser(mapping, appUserId, cancellationToken);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var customerDtoModel = await _customerAppService.GetById(id, cancellationToken);

            await _userAppService.DeleteUser((int)customerDtoModel.AppUserId, cancellationToken);
            await _customerAppService.Delete(id, cancellationToken);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
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

            if (model.BirthDate != null)
	            model.PersianCalender = model.BirthDate.ToPersianCalenderWithoutHour();
            else
	            model.PersianCalender = string.Empty;

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
