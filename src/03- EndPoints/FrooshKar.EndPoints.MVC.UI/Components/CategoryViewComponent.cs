using System.Threading;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Components
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly ICategoryAppService _categoryAppService;

		public CategoryViewComponent(ICategoryAppService categoryAppService)
		{
			_categoryAppService = categoryAppService;
		}

		public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
		{
			var getAllCategories = await _categoryAppService.GetAll(cancellationToken);
			var notDeletedCategories = getAllCategories.Where(x => x.IsDeleted == false).ToList();
			return View(notDeletedCategories);
		}
	}
}
