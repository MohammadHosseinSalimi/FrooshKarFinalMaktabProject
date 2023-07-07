using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FrooshKar.Domain.Core.Entities
{
	public class AppUser:IdentityUser<int>
	{
		#region Properties

		public bool IsDeleted { get; set; }

		#endregion


		#region NavigationProperties

		public Customer? Customer { get; set; }
		public Vendor? Vendor { get; set; }
		
		#endregion



	}
}
