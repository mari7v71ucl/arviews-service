using System;
using arviews_service.API.Infrastructure;
using arviews_service.API.Models;
using arviews_service.API.Models.Trendlog;
using arviews_service.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace arviews_service.API
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
            services.Configure<ArViewsServiceDatabaseSettings>(
                Configuration.GetSection(nameof(ArViewsServiceDatabaseSettings)));

            services.Configure<TrendlogServiceSettings>(
                Configuration.GetSection(nameof(TrendlogServiceSettings)));

            services.AddSingleton<IArViewsServiceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ArViewsServiceDatabaseSettings>>().Value);

            services.AddSingleton<ITrendlogServiceSettings>(sp =>
                sp.GetRequiredService<IOptions<TrendlogServiceSettings>>().Value);

            services.AddScoped<IARConfigService, ARConfigService>();
            services.AddScoped<IWorkspaceService, WorkspaceService>();

            services.AddHttpClient("TrendlogService", config =>
            {
                config.BaseAddress = new Uri(Configuration["IntegratedServices:Trendlog"]);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "arviews_service.API", Version = "v1" });
            });

            services.AddAutoMapper(
                options => options.AddProfile<MappingProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "arviews_service.API v1"));
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
