using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using liquidador_web.Data;
using liquidador_web.Interfaces;
using liquidador_web.Services;
using liquidador_web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using liquidador_web.Extra;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

namespace liquidador_web
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
            services.AddMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options => {
                options.Cookie.Name = "AspLiquidador.Session";
                options.IdleTimeout = System.TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            services.Configure<GoogleReCaptcha>(Configuration.GetSection("GoogleReCaptcha"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<LiquidadorUser>(config => config.SignIn.RequireConfirmedEmail = true).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IConsultas, Consultas>();
            services.AddScoped<IUserClaimsPrincipalFactory<LiquidadorUser>, UserClaimsPrincipalFactory<LiquidadorUser, IdentityRole>>();
            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var defaultCulture = new CultureInfo("es-CO");
            defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
            defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo>
                {
                    defaultCulture
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    defaultCulture
                }
            });
            
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "MyArea",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
