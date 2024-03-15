namespace AzureExample.Configuration;

public class PulumiSettings
{
    private string Env => Environment.GetEnvironmentVariable("AZUREEXAMPLE_ENVIRONMENT") ?? throw new Exception("AZUREEXAMPLE_ENVIRONMENT not set");
    public string NamePrefix => ConfigurationService.Configuration.GetSection("GeneralSettings")["ApplicationName"] + Env;
    public string Location => Environment.GetEnvironmentVariable("AZUREEXAMPLE_ARM_LOCATION") ?? throw new Exception("PULUMI_LOCATION not set");
    public bool Protect => bool.Parse(Environment.GetEnvironmentVariable("AZUREEXAMPLE_RG_PROTECT") ?? throw new Exception("AZUREEXAMPLE_RG_PROTECT not set"));
    public string SbnSkuName = Environment.GetEnvironmentVariable("AZUREEXAMPLE_SBN_SKU_NAME") ?? throw new Exception("AZUREEXAMPLE_SBN_SKU_NAME not set");
    public string SbnSkuTier = Environment.GetEnvironmentVariable("AZUREEXAMPLE_SBN_SKU_TIER") ?? throw new Exception("AZUREEXAMPLE_SBN_SKU_TIER not set");
    private string _azureClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID") ?? throw new Exception("AZURE_CLIENT_ID not set");
    private string _azureClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET") ?? throw new Exception("AZURE_CLIENT_SECRET not set");
    private string _azureTenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID") ?? throw new Exception("AZURE_TENANT_ID not set");
    private string _azureSubscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID") ?? throw new Exception("AZURE_SUBSCRIPTION_ID not set");
}
