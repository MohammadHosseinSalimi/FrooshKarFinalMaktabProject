using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Domain.Core.DTOs
{
	public class AppUserDtoModel
	{
		#region Properties

		public int Id { get; set; }
		public string? Email { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? Role { get; set; }
		public List<string> Roles { get; set; }
		public bool IsDeleted { get; set; }
		public Customer? Customer { get; set; }
		public Vendor? Vendor { get; set; }

		#endregion Properties


		public AppUserDtoModel()
		{
			IsDeleted = false;
		}


	}
}

