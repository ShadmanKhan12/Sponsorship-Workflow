FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# wwwroot/libs is gitignored; restore client assets before publish (fixes abp.css bundling on Render).
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates \
    && curl -fsSL https://deb.nodesource.com/setup_22.x | bash - \
    && apt-get install -y --no-install-recommends nodejs \
    && rm -rf /var/lib/apt/lists/*
RUN dotnet tool install -g Volo.Abp.Cli --version 10.4.0
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY . .

RUN dotnet restore src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj

WORKDIR /src/src/SponsorshipWorkflow.HttpApi.Host
RUN npm install && abp install-libs

WORKDIR /src
RUN dotnet publish src/SponsorshipWorkflow.HttpApi.Host/SponsorshipWorkflow.HttpApi.Host.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "SponsorshipWorkflow.HttpApi.Host.dll"]