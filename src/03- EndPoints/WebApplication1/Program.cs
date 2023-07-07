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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<FrooshKarDbContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=.;Initial Catalog=FrooshKarDb;TrustServerCertificate=True;Integrated Security=True;")));
			
			builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapping)));
			//builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMappingUI)));

			builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
			builder.Services.AddScoped<IAppUserService, AppUserService>();
			builder.Services.AddScoped<IAppUserAppService, AppUserAppService>();

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

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}