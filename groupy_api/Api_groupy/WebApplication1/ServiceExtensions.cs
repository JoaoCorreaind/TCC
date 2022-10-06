using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplication1.Models;
using WebApplication1.Tools.DataBase;

namespace WebApplication1
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection service)
        {
            var builder = service.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-. _@+^~´`";
                options.User.RequireUniqueEmail = true; //false
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //idem
                options.Password.RequireNonAlphanumeric = false; //true
                options.Password.RequireUppercase = false; //true;
                options.Password.RequireLowercase = false; //true;
                options.Password.RequireDigit = false; //true;
                options.Password.RequiredUniqueChars = 1; //1;
                options.Password.RequiredLength = 6; //6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //5
                options.Lockout.MaxFailedAccessAttempts = 999; //5
                options.Lockout.AllowedForNewUsers = true; //true		
                options.SignIn.RequireConfirmedEmail = true; //false
                options.SignIn.RequireConfirmedPhoneNumber = false; //false
                options.SignIn.RequireConfirmedAccount = false; //false
            });
            builder.AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
        }
    }
}
