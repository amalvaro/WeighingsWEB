using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

using Middleware;
using Database;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace WeighingsWEB
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddMvc().AddJsonOptions(options => {
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});

			services.AddTransient<UserContext> ();
			services.AddTransient<DbContext, Context> ();
			services.AddSingleton<Manager>();
            
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddControllersWithViews();
			services.AddSpaStaticFiles(configuration => {
				configuration.RootPath = "ClientApp/dist";
			});

			services.AddDistributedMemoryCache();
            // services.AddSession();

			services.AddDistributedMemoryCache();
				services.AddSession(options =>
				{
					options.Cookie.Name = ".Weighings.Session";
					options.IdleTimeout = TimeSpan.FromSeconds(3600);
					options.Cookie.IsEssential = true;
				});


		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSession();  
			app.UseMiddleware<AuthorizationMiddleware>();

			app.UseCookiePolicy();
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			// app.UseAuthentication();

			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";
				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
