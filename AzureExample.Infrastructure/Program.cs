using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.ServiceBus;
using Pulumi.AzureNative.ServiceBus.Inputs;
using System.Collections.Generic;
using AzureExample.Configuration;

return await Pulumi.Deployment.RunAsync(() =>
{
    var namePrefix = PulumiSettings.NamePrefix;

    // Create an Azure Resource Group
    var resourceGroup = new ResourceGroup($"{namePrefix}-rg", new ResourceGroupArgs
    {
        ResourceGroupName = $"{namePrefix}-rg",
        Location = PulumiSettings.Location
    }, new CustomResourceOptions
    {
        Protect = PulumiSettings.Protect
    });
    // Create an Azure Service Bus Namespace
    var serviceBusNamespace = new Namespace($"{namePrefix}-sbn", new NamespaceArgs
    {
        ResourceGroupName = resourceGroup.Name,
        Sku = new SkuArgs
        {
            Name = PulumiSettings.sbnSkuName,
            Tier = PulumiSettings.sbnSkuTier
        }
    });

    // Create a Service Bus Queue
    var orderSBQueue = new Queue($"{namePrefix}-order-queue", new QueueArgs
    {
        ResourceGroupName = resourceGroup.Name,
        NamespaceName = serviceBusNamespace.Name
    });

    // Export the primary connection string for the Service Bus Namespace
    var primaryConnectionString = Output.Tuple(resourceGroup.Name, serviceBusNamespace.Name).Apply(names =>
    {
        (string resourceGroupName, string namespaceName) = names;
        return GetNamespaceConnectionString.InvokeAsync(new GetNamespaceConnectionStringArgs
        {
            ResourceGroupName = resourceGroupName,
            NamespaceName = namespaceName,
            Alias = "RootManageSharedAccessKey"
        });
    });

    return new Dictionary<string, object?>
    {
        ["primaryConnectionString"] = primaryConnectionString
    };
});