namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models
{
	public class CategoryViewModel
	{ 
		public int Id { get; set; }

		public string? Title { get; set; } = null!;

		public string? CategoryImageName { get; set; }

		public string? CategoryImageUrl { get; set; }

		public IFormFile ImageFile { get; set; }

		public bool IsDeleted { get; set; }






	}
}
