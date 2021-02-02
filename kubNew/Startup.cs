using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace kubNew
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
            services.AddScoped<IEventRepository, EventCRUD>();
            services.AddScoped<IPlayerRepository, PlayersCRUD>();
            services.AddScoped<IJuryRepository, JuryCRUD>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
            services.AddScoped<PlayerCart>(sp => SessionCart.GetCart(sp));
            services.AddScoped<JuryCart>(sp => SessionJuryCart.GetJuryCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IPlayerRepository, Players>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "pagination",
                    pattern: "Event/Page:{page}",
                    defaults: new { Controller = "Event", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "Player/Rank:{rank}",
                    defaults: new { Controller = "Player", action = "Index" });

                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Event}/{action=Index}/{id?}");
            });
        }
    }
}
