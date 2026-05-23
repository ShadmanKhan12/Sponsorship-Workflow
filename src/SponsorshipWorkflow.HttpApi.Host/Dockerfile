FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj

RUN dotnet publish src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "SponsorshipWorkflow.HttpApi.Host.dll"]