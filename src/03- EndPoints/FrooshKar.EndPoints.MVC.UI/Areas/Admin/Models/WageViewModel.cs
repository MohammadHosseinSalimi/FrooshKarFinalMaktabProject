namespace FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models
{
	public class WageViewModel
	{
		public int Id { get; set; }
		public double BidWage { get; set; }
		public double FixedPriceWage { get; set; }
		public double TotalWage { get; set; }

		public string VendorFirstName { get; set;}
		public string VendorLastName { get; set;}



	}
}
