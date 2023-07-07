namespace FrooshKar.Domain.Core.Entities;

public partial class Vendor
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

	public int MedalId { get; set; }

	public string? Address { get; set; }

	public int? CityId { get; set; }
	public string? ProfileImageName { get; set; }

	public string? ProfileImageUrl { get; set; }


	public int? AppUserId { get; set; }


	#endregion

	#region NavigationProperties

	public virtual ICollection<BidProduct> BidProducts { get; set; } = new List<BidProduct>();

	public virtual City? City { get; set; }

	public virtual ICollection<FixedPriceProduct> FixedPriceProducts { get; set; } = new List<FixedPriceProduct>();

	public virtual Medal Medal { get; set; } = null!;
	public AppUser? AppUser { get; set; }


	#endregion

}
