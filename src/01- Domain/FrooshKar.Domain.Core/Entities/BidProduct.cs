namespace FrooshKar.Domain.Core.Entities;

public partial class BidProduct
{
	#region Properties
	public int Id { get; set; }

	public string? Title { get; set; }

	public int? BasePrice { get; set; }

	public int? FinalBidPrice { get; set; }

	public int? LastMaxModifiedPrice { get; set; }

	public string? Description { get; set; }

	public DateTime? StartBidTime { get; set; }

	public DateTime? EndBidTime { get; set; }

	public bool IsDeleted { get; set; }

	public DateTime? CreatedAt { get; set; }

	public int? CreatedBy { get; set; }

	public DateTime? LastModifiedAt { get; set; }

	public int? LastModifiedBy { get; set; }

	public DateTime? DeletedAt { get; set; }

	public int? DeletedBy { get; set; }

	public int? VendorId { get; set; }

	public bool IsValidByAdmin { get; set; }

	public int? CategoryId { get; set; }

	public int? WinnerCustomer { get; set; }
	public bool HasNoRecommend { get; set; } 

	public bool IsOpened { get; set; }

	public double? Wage { get; set; }
	#endregion

	#region NavigationProperties

	public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

	public virtual Category Category { get; set; } = null!;

	public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

	public virtual Vendor Vendor { get; set; } = null!;

	#endregion

}
