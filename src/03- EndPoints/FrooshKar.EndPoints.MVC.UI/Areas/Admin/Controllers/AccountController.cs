using FrooshKar.Domain.AppService.AppServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	public class AccountController : Controller
	{

		private readonly IAppUserAppService _appUserAppService;
		private readonly SeedIdentityData _identityData;
		private readonly IVendorAppService _vendorAppService;
		private readonly ICustomerAppService _customerAppService;
		public AccountController(SeedIdentityData identityData, IAppUserAppService appUserAppService, IVendorAppService vendorAppService, ICustomerAppService customerAppService)
		{
			_identityData = identityData;
			_appUserAppService = appUserAppService;
			_vendorAppService = vendorAppService;
			_customerAppService = customerAppService;
		}

		public async Task<IActionResult> Login(string email, CancellationToken cancellationToken)
		{
			await _identityData.Initialize(cancellationToken);

			return View();
		}
		public async Task<IActionResult> AccessDenied(CancellationToken cancellationToken)
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
		{

			var role = await _appUserAppService.FindUserRoleByEmail(model.Email, cancellationToken);
			if (role != String.Empty)
			{

				var appUserDtoModel = new AppUserDtoModel()
				{
					Email = model.Email,
					Password = model.Password,
					Role = role
				};


				var login = await _appUserAppService.Login(appUserDtoModel, cancellationToken);

				if (login.Succeeded && role == "Admin")
					return RedirectToAction("Index", "Home", new { area = "Admin" });
				else if (login.Succeeded && (role == "Vendor" || role == "Customer"))
					return RedirectToAction("Index", "Home", new { area = "" });
				else
					ModelState.AddModelError(String.Empty, "نام کاربری یا رمز عبور اشتباه است");
			}
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Login");
			}

			return View();
		}

		public async Task<IActionResult> Logout(CancellationToken cancellationToken)
		{

			await _appUserAppService.Logout();

			return RedirectToAction("Login", "Account", new { area = "Admin" });

		}



		public async Task<IActionResult> Register(CancellationToken cancellationToken)
		{
			return View();

		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
		{
			var appUserDto = new AppUserDtoModel()
			{
				Email = model.Email,
				Password = model.Password,
				ConfirmPassword = model.PasswordConfirmation,
				Role = model.Role
			};
			var regeister = await _appUserAppService.Create(appUserDto, cancellationToken);
			if (regeister == 0)
			{
				ModelState.AddModelError(String.Empty, "نام کاربری تکراری است");
			}
			else if (model.Role == "Vendor")
			{
				var vendor = new VendorDtoModel()
				{
					AppUserId = regeister
				};
				await _vendorAppService.CreateByAppUser(vendor, regeister, cancellationToken);
			}

			else if (model.Role == "Customer")
			{
				var customer = new CustomerDtoModel()
				{
					AppUserId = regeister
				};

				await _customerAppService.CreateByAppUser(customer, regeister, cancellationToken);
			}


			if (!ModelState.IsValid)
			{
				return RedirectToAction("Register");
			}

			return RedirectToAction("Login");


		}



	}
}

