using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Tools.DataBase;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Models.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using WebApplication1.settings;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Tools;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureIdentity();
            //services.AddIdentity<User, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.SignIn.RequireConfirmedAccount = true;
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/`´^~ ";
            //}).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            services.Configure<settings.GmailSettings>(Configuration.GetSection(nameof (settings.GmailSettings)));
            services.AddHttpContextAccessor();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddSingleton<IEmailRepository, EmailRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddIdentityCore<User>(q => q.User.RequireUniqueEmail = true);
            services.AddMvc();
            services.AddDbContext<Context>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
            });
            var key = Encoding.ASCII.GetBytes("E271BF74D722C245674175A739E7C");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddSignalR(e => {
                e.EnableDetailedErrors = true;
                e.MaximumReceiveMessageSize = 102400000;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Back end MOVE APP", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        

            //app.Use(async (httpContext, next) =>
            //{
            //    var accessToken = httpContext.Request.Query["access_token"];
            //    var accessToken1 = Functions.GetStringFromUrl(httpContext.Request.QueryString.ToString(), "access_token");
            //    var path = httpContext.Request.Path;
            //    if (!string.IsNullOrEmpty(accessToken) &&
            //        (path.StartsWithSegments("/chat")))
            //    {
            //        httpContext.Request.Headers["Authorization"] = "Bearer " + accessToken;
            //    }

            //    await next();
            //});

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
           
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            //app.UseCors(x =>
            //x.WithOrigins("http://localhost:4200")
            //.AllowAnyMethod()
            //.AllowAnyHeader()
            //.AllowCredentials());

            //app.UseCors(x =>
            //x.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader()
            //);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });

        }
    }

    
}
