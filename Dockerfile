### Multi-stage Dockerfile for SponsorshipWorkflow (ABP) - builds the HttpApi.Host project and runs it
### Base images use .NET 10 SDK/runtime. Ensure the host you deploy to supports these images.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore (helps caching). If your solution structure differs you can adjust paths.
COPY src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj src/SponsorshipWorkflow.HttpApi.Host/
COPY src/SponsorshipWorkflow.Application/SponsorshipWorkflow.Application.csproj src/SponsorshipWorkflow.Application/
COPY src/SponsorshipWorkflow.Application.Contracts/SponsorshipWorkflow.Application.Contracts.csproj src/SponsorshipWorkflow.Application.Contracts/
COPY src/SponsorshipWorkflow.Domain/SponsorshipWorkflow.Domain.csproj src/SponsorshipWorkflow.Domain/
COPY src/SponsorshipWorkflow.Domain.Shared/SponsorshipWorkflow.Domain.Shared.csproj src/SponsorshipWorkflow.Domain.Shared/
COPY src/SponsorshipWorkflow.EntityFrameworkCore/SponsorshipWorkflow.EntityFrameworkCore.csproj src/SponsorshipWorkflow.EntityFrameworkCore/
COPY src/SponsorshipWorkflow.HttpApi/SponsorshipWorkflow.HttpApi.csproj src/SponsorshipWorkflow.HttpApi/
COPY src/SponsorshipWorkflow.DbMigrator/SponsorshipWorkflow.DbMigrator.csproj src/SponsorshipWorkflow.DbMigrator/

RUN dotnet restore src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj

# Copy everything and publish
COPY . .
RUN dotnet publish src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Recommended for localization/invariant handling; adjust as needed.
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build /app/publish ./

EXPOSE 80

# Entrypoint
ENTRYPOINT ["dotnet", "SponsorshipWorkflow.HttpApi.Host.dll"]

# Notes for deployment on Render or other container hosts:
# - Provide database connection string via environment variable: ConnectionStrings__Default
#   Example: ConnectionStrings__Default=Host=<host>;Database=<db>;Username=<user>;Password=<pwd>;SSL Mode=Require;Trust Server Certificate=true
# - To run EF migrations automatically at startup you can either:
#   - Use the existing DbMigrator project and run it as a separate job/service, or
#   - Extend the entrypoint to run the migrator before starting the host (not included here).