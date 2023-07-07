using Microsoft.AspNetCore.Mvc;

namespace FrooshKar.EndPoints.MVC.UI.Models
{
	public class CartListViewModel
	{
		public List<CartViewModel>? CartList { get; set; } = new List<CartViewModel>();
	}
}
