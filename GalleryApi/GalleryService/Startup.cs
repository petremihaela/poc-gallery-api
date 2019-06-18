using GalleryService.Managers.Albums;
using GalleryService.Managers.Gallery;
using GalleryService.Managers.Photos;
using GalleryService.Managers.Token;
using GalleryService.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace GalleryService
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
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IGalleryManager, GalleryManager>();
            services.AddTransient<IAlbumManager, AlbumManager>();
            services.AddTransient<IPhotoManager, PhotoManager>();

            services.AddHttpClient("gallerySourceProvider", c =>
            {
                c.BaseAddress = new Uri(Configuration["GallerySourceProvider"]);
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Gallery API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseTokenAuthorization();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gallery API v1");
            });

            app.UseMvc();
        }
    }
}