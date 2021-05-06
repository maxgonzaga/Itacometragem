using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DinkToPdf;
using DinkToPdf.Contracts;
using Itacometragem.Library;
using Itacometragem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;

namespace Itacometragem
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ItacometragemContext>().AddDefaultTokenProviders();
            services.AddTransient<IItacometragemUnitOfWork, ItacometragemUnitOfWork>();
            services.AddScoped<Helper>(sp => new Helper(sp.GetRequiredService<IItacometragemUnitOfWork>()));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IReportService, ReportService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRouting(options => { 
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddDbContext<ItacometragemContext>(
                options => options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/filter/{driver}/{motive}/{car}/{initialdate}/{finaldate}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ride}/{action=Add}/{id?}");
            });
            ItacometragemContext.CreateAdminUser(app.ApplicationServices).Wait();
        }
    }
}
