using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrooshKar.Domain.Core.Enums;

namespace FrooshKar.Frameworks
{
	public static class ConvertGenderEnum
	{


		public static string ConvertGenderEnumToString(this GenderEnum gender)
		{

			switch (gender)
			{
				case GenderEnum.Male:
					return "مرد";
				case GenderEnum.Female:
					return "زن";
			}

			return null;


		}




	}
}
