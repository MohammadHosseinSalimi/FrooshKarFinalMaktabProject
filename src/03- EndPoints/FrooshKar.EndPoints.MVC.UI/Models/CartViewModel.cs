using System.ComponentModel.DataAnnotations;
using FrooshKar.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Models
{
	public class CartViewModel
	{

		#region Properties

		public int Id { get; set; }
		//[Remote("Count","Cart",AdditionalFields = "productId")]
		public int? Count { get; set; }

		public int? FactorId { get; set; }

		public int? CustomerId { get; set; }

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

		public virtual Customer? Customer { get; set; } = null!;

		public virtual Factor? Factor { get; set; }

		public virtual FixedPriceProduct? FixedPriceProduct { get; set; }
		#endregion


	}
}
