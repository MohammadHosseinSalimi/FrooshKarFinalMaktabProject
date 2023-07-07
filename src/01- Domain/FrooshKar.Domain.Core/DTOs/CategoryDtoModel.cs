using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class CategoryDtoModel
	{
		#region Properties
		public int Id { get; set; }

		public string? Title { get; set; } = null!;

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }

		public string? CategoryImageName { get; set; }

		public string? CategoryImageUrl { get; set; }


		#endregion

		#region NavigationProperties

		public virtual ICollection<BidProduct> BidProducts { get; set; } = new List<BidProduct>();

		public virtual ICollection<FixedPriceProduct> FixedPriceProducts { get; set; } = new List<FixedPriceProduct>();

		#endregion

		public CategoryDtoModel()
		{ 
			IsDeleted = false;
		}


	}
}
