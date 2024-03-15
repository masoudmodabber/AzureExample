namespace AzureExample.Configuration;
public class AzureSettings
{
    public string AzureConnectionString => Environment.GetEnvironmentVariable("AZUREEXAMPLE_ENVIRONMENT") ?? throw new Exception("AZUREEXAMPLE_ENVIRONMENT not set");

    public string? OrderQueue { get; set; }
    //TODO: threw an exception if the value is not set

}