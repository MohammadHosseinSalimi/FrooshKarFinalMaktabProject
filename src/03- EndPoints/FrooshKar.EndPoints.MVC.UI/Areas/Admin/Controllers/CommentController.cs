using System.Data;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController : Controller
	{
		private readonly ICommentAppService _commentAppService;

		public CommentController(ICommentAppService commentAppService)
		{
			_commentAppService = commentAppService;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var model = await _commentAppService.GetAll(cancellationToken);

			return View(model);
		}
		
		public async Task<IActionResult> AcceptComment(int id, CancellationToken cancellationToken)
		{
			var model = await _commentAppService.GetById(id, cancellationToken);
			model.IsValidByAdmin = true;
			await _commentAppService.Update(model, cancellationToken);
			return RedirectToAction(nameof(Index));

		}
		public async Task<IActionResult> RejectComment(int id, CancellationToken cancellationToken)
		{
			var model = await _commentAppService.GetById(id, cancellationToken);
			model.IsValidByAdmin = false;
			await _commentAppService.Update(model, cancellationToken);
			return RedirectToAction(nameof(Index));

		}

	}
}
