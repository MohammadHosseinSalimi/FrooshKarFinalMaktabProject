using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class BidDtoModel
	{
		#region Properties

		public int Id { get; set; }

		public int CustomerId { get; set; }

		public int BidProductId { get; set; }

		public int? BidPrice { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }

		#endregion

		#region NavigationProperties

		public virtual BidProduct BidProduct { get; set; } = null!;

		public virtual Customer Customer { get; set; } = null!;

		#endregion


		public BidDtoModel()
		{
			CreatedAt=DateTime.Now;
			IsDeleted = false;
		}


	}
}
