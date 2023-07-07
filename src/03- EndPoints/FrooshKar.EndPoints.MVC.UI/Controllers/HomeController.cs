using System.Diagnostics;
using System.Runtime.CompilerServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.EndPoints.MVC.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Controllers
{
    [AllowAnonymous]
	public class HomeController : Controller
	{

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			return View();
		}



    }

}