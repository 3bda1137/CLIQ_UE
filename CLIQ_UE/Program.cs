using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//Add Service DbContext
			builder.Services.AddDbContext<ApplicationContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
			});
			//Add Service Identity
			builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
							option =>
							{
								//setting of Password
								option.Password.RequireNonAlphanumeric = false;
								option.Password.RequireDigit = true;
								option.Password.RequiredLength = 6;
							}).AddEntityFrameworkStores<ApplicationContext>()
							.AddDefaultTokenProviders();//to generat token

			//register My Services
			builder.Services.AddScoped<IUserServices, UserServices>();

			builder.Services.AddSignalR();
			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			//app.UseAuthentication();
			app.UseAuthorization();


			app.MapHub<ChatIndividualHub>("/ChatIndividual");
			app.MapHub<OnlineUsersHub>("/OnlineUsers");


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Register}/{id?}");

			app.Run();
		}
	}
}
