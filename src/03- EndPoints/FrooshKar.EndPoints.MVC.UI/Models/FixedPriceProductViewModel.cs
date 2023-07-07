using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.EndPoints.MVC.UI.Models
{
	public class FixedPriceProductViewModel
	{
		public int Id { get; set; }
		public string? Title { get; set; }

		public int? UnitPrice { get; set; }

		public int? Quantity { get; set; }

		public string? Description { get; set; }

		public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();

		public int? VendorId { get; set; }

		public bool IsValidByAdmin { get; set; }

		public int? CategoryId { get; set; }

		public bool IsDeleted { get; set; }

		public List<CategoryDtoModel> Categories { get; set; } = new List<CategoryDtoModel>();

		public List<VendorDtoModel> Vendors { get; set; } = new List<VendorDtoModel>();
		public virtual Vendor? Vendor { get; set; } = null!;
		public virtual Category? Category { get; set; } = null!;


	}
}
