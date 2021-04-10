FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

ENV http_proxy 'http://172.16.20.207:3128'
ENV https_proxy 'http://172.16.20.207:3128' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'

# Copy csproj and restore as distinct layers
COPY Presentaion/CRCIS.Web.INoor.CRM.WebApi/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Presentaion/CRCIS.Web.INoor.CRM.WebApi/. ./
RUN dotnet publish -c Release -o out

ENV http_proxy 'http://172.16.20.207:3128'
ENV https_proxy 'http://172.16.20.207:3128' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out ./
EXPOSE 80
# EXPOSE 5000
# EXPOSE 5001
#ENV ASPNETCORE_URLS=http://+:8011
# ENV ASPNETCORE_HTTPS_PORT=5001
ENTRYPOINT ["dotnet", "CRCIS.Web.INoor.CRM.WebApi.dll"]
