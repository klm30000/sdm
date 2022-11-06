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

using Microsoft.EntityFrameworkCore;

using EHRApp;
using EHRRepository;
using EHRRepository.DbContexts;
using EHRDomain;
using Microsoft.Extensions.Logging;

namespace EHR
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
            services.AddLogging(cfg => {
                cfg.AddLog4Net();
            });
            services.AddControllersWithViews().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
            services.AddSwaggerGen();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EHRDbContext>(options =>
            {
                options.UseSqlite(connectionString, x => x.MigrationsAssembly("EHR"));
                options.UseLoggerFactory(LoggerFactory.Create(cfg => cfg.AddLog4Net()));
                options.EnableSensitiveDataLogging();
            });

            //dependency injection
            services.AddScoped<UserRepository>();
            services.AddScoped<UserApp>();
            services.AddScoped<PatientRepository>();
            services.AddScoped<PatientApp>();
            services.AddScoped<PatientCaseRepository>();
            services.AddScoped<PatientCaseApp>();
            services.AddScoped<PathologyRepository>();
            services.AddScoped<PathologyApp>();
            services.AddScoped<PathologyTumorMarkerRepository>();
            services.AddScoped<TumorMarkerRepository>();
            services.AddScoped<TumorMarkerApp>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PatientCase}/{action=Index}/{id?}");
            });
        }
    }
}
