namespace FrooshKar.Domain.Core.Entities;

public partial class Comment
{

	#region Properties

	public int Id { get; set; }

	public int FactorId { get; set; }

	public bool IsValidByAdmin { get; set; }

	public bool IsDeleted { get; set; }

	public DateTime? CreatedAt { get; set; }

	public int? CreatedBy { get; set; }

	public DateTime? LastModifiedAt { get; set; }

	public int? LastModifiedBy { get; set; }

	public DateTime? DeletedAt { get; set; }

	public int? DeletedBy { get; set; }

	public string? Description { get; set; }

	#endregion

	#region NavigationProperties

	public virtual Factor Factor { get; set; } = null!;

	#endregion

}
