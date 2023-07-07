using System.Configuration;
using System.Reflection;
using FrooshKar.Domain.AppService.AppServices;
using FrooshKar.Domain.Core.Contracts.ApplicationService;
using FrooshKar.Domain.Core.Contracts.Repository;
using FrooshKar.Domain.Core.Contracts.Service;
using FrooshKar.Domain.Core.Entities;
using FrooshKar.Domain.Service.Services;
using FrooshKar.Infrastructuers.Data.Repositories.AutoMapper;
using FrooshKar.Infrastructuers.Data.Repositories.Repositories;
using FrooshKar.Infrastructures.Db.Sql.EF.DataBase;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Assembly = System.Reflection.Assembly;

namespace FrooshKar.EndPoints.MVC.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews()
				.AddRazorRuntimeCompilation();




			builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapping)));
			builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMappingUI)));

			builder.Services.AddDbContext<FrooshKarDbContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("FrooshKar")));

			builder.Services.AddIdentity<AppUser, IdentityRole<int>>(
					options =>
					{
						options.SignIn.RequireConfirmedPhoneNumber = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Lockout.MaxFailedAccessAttempts = 5;
						options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
						options.Password.RequireDigit = false;
						options.Password.RequiredLength = 4;
						options.Password.RequireNonAlphanumeric = false;
					}
				)
				.AddEntityFrameworkStores<FrooshKarDbContext>();

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Admin/Account/Login";
				options.AccessDeniedPath = "/Admin/Account/AccessDenied";
			});

			builder.Services.AddHangfire(x =>
			{
				x.UseSqlServerStorage(builder.Configuration.GetConnectionString("FrooshKar"));
			});
			builder.Services.AddHangfireServer();

			var picConfigs = builder.Configuration.GetSection("PicConfigs").Get<PicConfigs>();
			builder.Services.AddSingleton(picConfigs);

			builder.Services.AddHttpContextAccessor();


			builder.Services.AddScoped<SeedIdentityData>();


			builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
			builder.Services.AddScoped<IBidRepository, BidRepository>();
			builder.Services.AddScoped<IBidProductRepository, BidProductRepository>();
			builder.Services.AddScoped<ICartRepository, CartRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ICityRepository, CityRepository>();
			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
			builder.Services.AddScoped<IFactorRepository, FactorRepository>();
			builder.Services.AddScoped<IFixedPriceProductRepository, FixedPriceProductRepository>();
			builder.Services.AddScoped<IMedalRepository, MedalRepository>();
			builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
			builder.Services.AddScoped<IVendorRepository, VendorRepository>();

			builder.Services.AddScoped<IAppUserService, AppUserService>();
			builder.Services.AddScoped<IBidService, BidService>();
			builder.Services.AddScoped<IBidProductService, BidProductService>();
			builder.Services.AddScoped<ICartService, CartService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<ICityService, CityService>();
			builder.Services.AddScoped<ICommentService, CommentService>();
			builder.Services.AddScoped<ICustomerService, CustomerService>();
			builder.Services.AddScoped<IFactorService, FactorService>();
			builder.Services.AddScoped<IFixedPriceProductService, FixedPriceProductService>();
			builder.Services.AddScoped<IMedalService, MedalService>();
			builder.Services.AddScoped<IProductImageService, ProductImageService>();
			builder.Services.AddScoped<IVendorService, VendorService>();


			builder.Services.AddScoped<IAppUserAppService, AppUserAppService>();
			builder.Services.AddScoped<IBidAppService, BidAppService>();
			builder.Services.AddScoped<IBidProductAppService, BidProductAppService>();
			builder.Services.AddScoped<ICartAppService, CartAppService>();
			builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
			builder.Services.AddScoped<ICityAppService, CityAppService>();
			builder.Services.AddScoped<ICommentAppService, CommentAppService>();
			builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
			builder.Services.AddScoped<IFactorAppService, FactorAppService>();
			builder.Services.AddScoped<IFixedPriceProductAppService, FixedPriceProductAppService>();
			builder.Services.AddScoped<IMedalAppService, MedalAppService>();
			builder.Services.AddScoped<IProductImageAppService, ProductImageAppService>();
			builder.Services.AddScoped<IVendorAppService, VendorAppService>();



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapAreaControllerRoute(
				name: "Areas",
				areaName: "Admin",
				pattern: "Admin/{controller=Account}/{action=Login}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.UseHangfireDashboard();

			app.Run();
		}
	}
}