using Invoice.Api.Extensions;
using Invoice.Api.Middlewares;
using Invoice.Application;
using Invoice.Infra;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

// Add services to the container.

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin());
});

services.AddAutoMapperSetup();
services.AddControllersWithViews()
        .AddNewtonsoftJson();
services.AddSwagger();
services.AddJwtAuthentication(configuration);
services.AddDatabase(configuration);
services.AddUnitOfWork();
services.AddInfrastructure();
services.AddApplication();
services.AddCache(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{ 
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Core Api v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<RequestDiagnosticsMiddleware>();
app.UseCors("CorsPolicy");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
