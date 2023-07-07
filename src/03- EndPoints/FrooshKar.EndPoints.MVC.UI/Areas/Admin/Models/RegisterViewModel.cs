using System.ComponentModel.DataAnnotations;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models
{
	public class RegisterViewModel
	{
		public string? Role { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		[Compare(nameof(Password), ErrorMessage = "پسورد وارد شده با تکرار آن برابر نیست")]
		public string? PasswordConfirmation { get; set; }
		public string? Email { get; set; }



	}
}	
