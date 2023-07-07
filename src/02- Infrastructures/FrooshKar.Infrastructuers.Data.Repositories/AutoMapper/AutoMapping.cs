using AutoMapper;
using FrooshKar.Domain.Core.DTOs;
using FrooshKar.Domain.Core.Entities;

namespace FrooshKar.Infrastructuers.Data.Repositories.AutoMapper
{
	public class AutoMapping:Profile
	{

		public AutoMapping()
		{

			CreateMap<AppUserDtoModel, AppUser>();
			CreateMap<AppUser, AppUserDtoModel>();

			CreateMap<BidProductDtoModel, BidProduct>();
			CreateMap<BidProduct, BidProductDtoModel>();

			CreateMap<BidDtoModel, Bid>();
			CreateMap<Bid, BidDtoModel>();

			CreateMap<CartDtoModel, Cart>();
			CreateMap<Cart, CartDtoModel>();

			CreateMap<CategoryDtoModel, Category>();
			CreateMap<Category, CategoryDtoModel>();

			CreateMap<CityDtoModel, City>();
			CreateMap<City, CityDtoModel>();

			CreateMap<CommentDtoModel, Comment>();
			CreateMap<Comment, CommentDtoModel>();

			CreateMap<CustomerDtoModel, Customer>();
			CreateMap<Customer, CustomerDtoModel>();

			CreateMap<FactorDtoModel, Factor>();
			CreateMap<Factor, FactorDtoModel>();

			CreateMap<FixedPriceProductDtoModel, FixedPriceProduct>();
			CreateMap<FixedPriceProduct, FixedPriceProductDtoModel>();


			CreateMap<MedalDtoModel, Medal>();
			CreateMap<Medal, MedalDtoModel>();

			CreateMap<ProductImageDtoModel, ProductImage>();
			CreateMap<ProductImage, ProductImageDtoModel>();


			CreateMap<VendorDtoModel, Vendor>();
			CreateMap<Vendor, VendorDtoModel>();






		}


	}
}
