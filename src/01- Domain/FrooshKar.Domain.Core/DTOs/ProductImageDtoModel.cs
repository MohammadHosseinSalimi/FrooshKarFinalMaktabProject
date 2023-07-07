using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class ProductImageDtoModel
	{
		#region Properties
		public int Id { get; set; }

		public string? Title { get; set; }

		public string? Url { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }

		public int? FixedPriceProductId { get; set; }

		public int? BidProductId { get; set; }


		#endregion

		#region NavigationProperties
		public virtual BidProduct? BidProduct { get; set; }

		public virtual FixedPriceProduct? FixedPriceProduct { get; set; }
		#endregion


		public ProductImageDtoModel()
		{
			//CreatedAt = DateTime.Now;
			IsDeleted = false;
		}
	}
}
