using AutoMapper;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.EndPoints.MVC.UI.Areas.Admin.Models;
using FrooshKar.EndPoints.MVC.UI.Models;

namespace FrooshKar.EndPoints.MVC.UI
{
	public class AutoMappingUI : Profile
	{

		public AutoMappingUI()
		{
			CreateMap<CategoryDtoModel, CategoryViewModel>().ReverseMap();
			CreateMap<CustomerDtoModel, CustomerViewModel>().ReverseMap();
			CreateMap<CustomerDtoModel, AppUserDtoModel>().ReverseMap();
			CreateMap<VendorDtoModel, VendorViewModel>().ReverseMap();
			CreateMap<VendorDtoModel, AppUserDtoModel>().ReverseMap();
			CreateMap<FixedPriceProductDtoModel, FixedPriceProductViewModel>().ReverseMap();
			CreateMap<CartDtoModel,CartViewModel>().ReverseMap();
			CreateMap<Cart,CartViewModel>().ReverseMap();

		}

	}
}
