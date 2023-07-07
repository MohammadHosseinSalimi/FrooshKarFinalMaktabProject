using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class CustomerDtoModel
	{
		#region Properties

		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? LastModifiedAt { get; set; }

		public int? LastModifiedBy { get; set; }

		public DateTime? DeletedAt { get; set; }

		public int? DeletedBy { get; set; }

		public string? Gender { get; set; }

		public string? Address { get; set; }

		public int? CityId { get; set; }

		public string? ProfileImageName { get; set; }

		public string? ProfileImageUrl { get; set; }

		public int? AppUserId { get; set; }

		#endregion

		#region NavigationProperties

		public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

		public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

		public virtual City? City { get; set; }
		public AppUser? AppUser { get; set; }


		#endregion

		public CustomerDtoModel()
		{
			IsDeleted = false;
		}
	}
}
