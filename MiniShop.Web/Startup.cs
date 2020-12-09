using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniShop.App;
using MiniShop.EF;
using MiniShop.Identity;
using MiniShop.Identity.Data;
using MiniShop.Infrastructure;

namespace MiniShop.Web
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
            #region Identity
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/admin/auth/login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region Mapper            
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryProfileMapping());
                cfg.AddProfile(new ProductProfileMapping());
                cfg.AddProfile(new AreaProfileMapping());
                cfg.AddProfile(new BlogProfileMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            // Add Db Context
            services.AddDbContext<MiniShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MiniShopConnection")));

            #region Registry dependency
            // Register DbContext
            services.AddScoped<MiniShopContext>();
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register RepositoryBase
            services.AddScoped<IRepositoryBase, RepositoryBase>();

            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IBlogService, BlogService>();
            #endregion

            // Config setting 
            var infoServerConfig =Configuration.GetSection("InfoServer");
            services.Configure<InfoServerConfig>(infoServerConfig);

            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] {
                    UnicodeRanges.All
                }));

            //register kendo
            services.AddKendo();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //Migrate database
            MigrateDatabaseAuto.Migrate(app, userManager, roleManager);

            // Add Logfile
            loggerFactory.AddFile(Configuration.GetSection("Logging:LogPath").Value);
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
            //        System.IO.Path.Combine(env.ContentRootPath, "wwwroot")),
            //    RequestPath = "/wwwroot"
            //}); ;


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
