using System.ComponentModel.DataAnnotations;

namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models
{
	public class LoginViewModel
	{
		public string? Role { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string? Email { get; set; }



	}
}	
