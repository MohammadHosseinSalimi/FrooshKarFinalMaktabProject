using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class CommentDtoModel
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

		public CommentDtoModel()
		{
			CreatedAt = DateTime.Now;
			IsDeleted = false;
			IsValidByAdmin = false;
		}
	}
}
