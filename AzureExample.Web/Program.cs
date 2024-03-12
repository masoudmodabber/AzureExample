using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using AzureExample.Configuration.Settings;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
var app = builder.Build();

ConfigurePipeline(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllersWithViews();
    builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection("AzureSettings"));
    builder.Services.AddSingleton(x =>
    {
        var azureSettings = x.GetRequiredService<IOptions<AzureSettings>>().Value;
        return new ServiceBusClient(azureSettings.ConnectionString);
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

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}