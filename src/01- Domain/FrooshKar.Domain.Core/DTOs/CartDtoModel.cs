using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class CartDtoModel
	{

		#region Properties

		public int Id { get; set; }

		public int? Count { get; set; }

		public int? FactorId { get; set; }

		public int CustomerId { get; set; }

		public bool? IsFinished { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }
        public int? FixedPriceProductId { get; set; }

        #endregion

        #region NavigationProperties

        public virtual Customer Customer { get; set; } = null!;

		public virtual Factor? Factor { get; set; }

        public virtual FixedPriceProduct? FixedPriceProduct { get; set; }

        #endregion

        public CartDtoModel()
		{
			CreatedAt=DateTime.Now;
			IsDeleted = false;
			IsFinished = false;
		}

	}
}
