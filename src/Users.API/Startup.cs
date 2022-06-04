using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.API.Extensions;
using Users.API.Middlewares;

namespace Users.API
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

            services.AddCors();

            services.AddAutoMapperSetup();

            services.AddControllers()
                    .AddNewtonsoftJson();

            services.AddSwagger();

            services.AddConfiguration(Configuration);

            services.AddJwtAuthentication(Configuration);            

            services.AddDatabase(Configuration);

            services.AddUnitOfWork();

            services.AddRepositories();

            services.AddCache();

            services.AddBusinessServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();                                      
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users Core API v1"));
            }

            app.UseRouting();

            app.UseCors(x => x.WithOrigins("http://localhost:4201", "http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .SetIsOriginAllowed(origin => true)
                              .AllowCredentials());

            app.UseMiddleware<RequestDiagnosticsMiddleware>();              

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
