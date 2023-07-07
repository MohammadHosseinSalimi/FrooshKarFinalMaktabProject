﻿namespace FrooshKar.Domain.Core.Entities;

public partial class Medal
{
	#region Properties

	public int Id { get; set; }

	public string? Title { get; set; }

	public double? WagePercent { get; set; }
	public int? SellLimit { get; set; }

	public bool IsDeleted { get; set; }

	public DateTime? CreatedAt { get; set; }

	public int? CreatedBy { get; set; }

	public DateTime? LastModifiedAt { get; set; }

	public int? LastModifiedBy { get; set; }

	public DateTime? DeletedAt { get; set; }

	public int? DeletedBy { get; set; }

	#endregion

	#region NavigationProperties

	public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();

	#endregion

}
