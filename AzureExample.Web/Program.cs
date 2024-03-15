using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using AzureExample.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Get the configuration from the ConfigurationService
var configuration = ConfigurationService.Configuration;

ConfigureServices(builder, configuration);
var app = builder.Build();

ConfigurePipeline(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder, IConfiguration configuration)
{
    builder.Services.AddControllersWithViews();
    builder.Services.Configure<AzureSettings>(configuration.GetSection("AzureSettings"));
    builder.Services.AddSingleton(x =>
    {
        var azureSettings = x.GetRequiredService<IOptions<AzureSettings>>().Value;
        return new ServiceBusClient(azureSettings.AzureConnectionString);
    });
}

void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}