using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MW.Application;
using MW.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MW.API
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
            services.AddDbContext<MiddleWareDBContext>(options => 
             options.UseNpgsql(Configuration.GetConnectionString("MiddleWareConnect")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceInit, ServiceInit>();
            services.AddScoped<IMappingHandlers, MappingHandlers>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
            services.AddResponseCompression(options =>
                {
                    options.MimeTypes = new[]
                    {
                    "application/xml",
                    "application/json",
                    "text/json",
                    "image/svg+xml"
                };
                options.EnableForHttps = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c => 
            { 
                c.SwaggerDoc("v1", new Info 
                { 
                    Title = "Rensource Middleware API Documentation", 
                    Version = "v1" 
                }); 
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(Constant.ErrorPage);
                app.UseHsts();
            }
            
            app.UseSwagger(c => 
            { 
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value); 
            }); 
            app.UseSwaggerUI(c => 
            { 
                c.RoutePrefix = "swagger"; // serve the UI at root 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs"); 
            }); 

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                "img-src 'host'" +
                "connect-src 'none'" +
                "object-src 'none'" +
                "media-src 'none'" +
                "form-action 'self'" +
                "default-src 'self'");
                await next();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=LandingPage}/{action=Index}/{id?}");
            });
            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MiddleWareDBContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
