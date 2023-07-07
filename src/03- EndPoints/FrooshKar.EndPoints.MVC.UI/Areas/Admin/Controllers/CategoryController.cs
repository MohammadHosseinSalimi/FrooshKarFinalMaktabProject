using AutoMapper;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryAppService _categoryAppService;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IMapper _mapper;
		private readonly PicConfigs _picConfigs;

		public CategoryController(ICategoryAppService categoryAppService, IWebHostEnvironment hostingEnvironment, IMapper mapper, PicConfigs picConfigs)
		{
			_categoryAppService = categoryAppService;
			_hostingEnvironment = hostingEnvironment;
			_mapper = mapper;
			_picConfigs = picConfigs;
		}



		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var record = await _categoryAppService.GetAll(cancellationToken);
			return View(_mapper.Map<List<CategoryViewModel>>(record));
		}


		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			await _categoryAppService.Delete(id, cancellationToken);
			return RedirectToAction("Index");

		}

		[HttpGet]
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			return View();

		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryViewModel model,
			CancellationToken cancellationToken)
		{

			var categoryImageName = await AddImage.AddSingleImage(_picConfigs.CategoryImageFolderName, model.ImageFile,
				_hostingEnvironment.WebRootPath, cancellationToken);
			model.CategoryImageName = categoryImageName;
			model.CategoryImageUrl = "/" + _picConfigs.CategoryImageFolderName +"/"+ categoryImageName;

			await _categoryAppService.Create(_mapper.Map<CategoryDtoModel>(model), cancellationToken);
			return RedirectToAction("Index");

		}



		public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
		{
			var record = await _categoryAppService.GetById(id, cancellationToken);
			return View(_mapper.Map<CategoryViewModel>(record));
		}

		[HttpPost]
		public async Task<IActionResult> Update(CategoryViewModel model, CancellationToken cancellationToken)
		{
			var categoryImageName = await AddImage.AddSingleImage(_picConfigs.CategoryImageFolderName, model.ImageFile,
				_hostingEnvironment.WebRootPath, cancellationToken);
			model.CategoryImageName = categoryImageName;
			model.CategoryImageUrl = "/" + _picConfigs.CategoryImageFolderName + "/" + categoryImageName;


			var command = _mapper.Map<CategoryDtoModel>(model);

			await _categoryAppService.Update(command, cancellationToken);

			return RedirectToAction("Index");
		}




	}



}
