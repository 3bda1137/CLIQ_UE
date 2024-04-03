using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
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
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
            builder.Services.AddScoped<IReactionService, ReactionService>();

            builder.Services.AddScoped<IViewRepository, ViewRepository>();
            builder.Services.AddScoped<IViewService, ViewService>();

            builder.Services.AddScoped<IEditUserServices, EditUserServices>();
            builder.Services.AddScoped<IEditUserRepository, EditUserRepository>();
            builder.Services.AddScoped<IUserLikeCommentRepository, UserLikeCommentRepository>();
            builder.Services.AddScoped<IUserLikeCommentService, UserLikeCommentService>();
            

            builder.Services.AddSignalR();
            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
                pattern: "{controller=HomePage}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
