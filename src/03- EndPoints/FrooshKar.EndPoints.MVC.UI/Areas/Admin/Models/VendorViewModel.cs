using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Enums;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models
{
	public class VendorViewModel
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		public bool IsDeleted { get; set; }

		public string? Gender { get; set; }
		public GenderEnum GenderEnum { get; set; }

		public string? Address { get; set; }

		public string City { get; set; }

		public int CityId { get; set; }

		public List<CityDtoModel> Cities { get; set; }


		public string PersianCalender { get; set; }

		public string? ProfileImageName { get; set; }

		public string? ProfileImageUrl { get; set; }

		public int? AppUserId { get; set; }
		public IFormFile ImageFile { get; set; }

		public int MedalId { get; set; }
		public string Medal { get; set; }
		public List<MedalDtoModel> Medals { get; set; }

		public string? Role { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string? Email { get; set; }




	}
}
