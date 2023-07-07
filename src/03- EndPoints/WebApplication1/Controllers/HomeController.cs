using System.Diagnostics;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IAppUserAppService _appUserAppService;

		public HomeController(ILogger<HomeController> logger, IAppUserAppService appUserAppService)
		{
			_logger = logger;
			_appUserAppService = appUserAppService;
		}

		public IActionResult Index(CancellationToken cancellationToken)
		{
			var appUserDtoModel = new AppUserDtoModel()
			{
				ConfirmPassword = "13741374",
				Password = "13741374",
				Email = "mh@gmail.com",
				UserName = "mh@gmail.com",
				Role = "Customer"
			};

			_appUserAppService.Create(appUserDtoModel, cancellationToken);


			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}