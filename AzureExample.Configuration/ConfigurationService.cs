using Microsoft.Extensions.Configuration;

namespace AzureExample.Configuration;
public static class ConfigurationService
{
    private const string ConfigFilePath = "appsettings.json";

    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(ConfigFilePath, optional: false, reloadOnChange: true)
        .Build();
    
}
