using FrooshKar.Domain.Core.DTOs;

namespace FrooshKar.EndPoints.MVC.UI.Models
{
	public class BidProductViewModel
	{
		public int Id { get; set; }
		public string? Title { get; set; }

		public int? BasePrice { get; set; }

		public int? FinalBidPrice { get; set; }

		public int? LastMaxModifiedPrice { get; set; }

		public string? Description { get; set; }

		public DateTime? StartBidTime { get; set; }

		public DateTime? EndBidTime { get; set; }

		public bool IsDeleted { get; set; }


		public int VendorId { get; set; }


		public int CategoryId { get; set; }
		public int WinnerCustomer { get; set; }

		public List<CategoryDtoModel> Categories { get; set; } = new List<CategoryDtoModel>();

		public List<VendorDtoModel> Vendors { get; set; } = new List<VendorDtoModel>();

		public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();

		public string StartBidTimePersianCalender { get; set; }
		public string EndBidTimePersianCalender { get; set; }


	}
}
