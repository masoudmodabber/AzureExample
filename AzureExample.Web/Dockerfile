# Build stage using .NET 7.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project files and restore NuGet packages
COPY AzureExample.Configuration/AzureExample.Configuration.csproj AzureExample.Configuration/
COPY AzureExample.Web/AzureExample.Web.csproj AzureExample.Web/
RUN dotnet restore AzureExample.Web/AzureExample.Web.csproj --use-current-runtime 

# Copy source code and publish the application
COPY AzureExample.Configuration/ AzureExample.Configuration/
COPY AzureExample.Web/ AzureExample.Web/
WORKDIR /src/AzureExample.Web
RUN dotnet publish --configuration Release --use-current-runtime --self-contained false --no-restore -o /app

# Runtime stage using .NET 7.0 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app .

# Set the entrypoint command
ENTRYPOINT ["dotnet", "AzureExample.Web.dll"]