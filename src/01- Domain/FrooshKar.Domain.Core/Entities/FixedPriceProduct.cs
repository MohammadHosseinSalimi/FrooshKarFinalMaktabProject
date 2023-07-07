namespace FrooshKar.Domain.Core.Entities;

public partial class FixedPriceProduct
{

	#region Properties

	public int Id { get; set; }

	public string? Title { get; set; }

	public int? UnitPrice { get; set; }

	public int? Quantity { get; set; }

	public string? Description { get; set; }

	public bool IsDeleted { get; set; }

	public DateTime? CreatedAt { get; set; }

	public int? CreatedBy { get; set; }

	public DateTime? LastModifiedAt { get; set; }

	public int? LastModifiedBy { get; set; }

	public DateTime? DeletedAt { get; set; }

	public int? DeletedBy { get; set; }

	public int? CartId { get; set; }

	public int? VendorId { get; set; }

	public bool IsValidByAdmin { get; set; }

	public int? CategoryId { get; set; }

	#endregion

	#region NavigationProperties

	public virtual Category? Category { get; set; } = null!;

	public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Vendor? Vendor { get; set; } = null!;

	#endregion


}
