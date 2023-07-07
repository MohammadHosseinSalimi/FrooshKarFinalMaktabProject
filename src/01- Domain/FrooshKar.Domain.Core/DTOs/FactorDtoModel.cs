using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class FactorDtoModel
	{
		#region Properties

		public int Id { get; set; }

		public int? TotalPrice { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }

		public double? Wage { get; set; }

		#endregion

		#region NavigationProperties

		public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

		public virtual Comment? Comment { get; set; }

		#endregion

		public FactorDtoModel()
		{
			CreatedAt = DateTime.Now;
			IsDeleted = false;
		}
	}
}
