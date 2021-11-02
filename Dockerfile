FROM hub.inoor.ir/common/dotnet/sdk:5.0 AS build-env
WORKDIR /app

ENV http_proxy 'http://172.16.20.207:3128'
ENV https_proxy 'http://172.16.20.207:3128' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'

# Copy csproj and restore as distinct layers
COPY . ./
WORKDIR /app/Presentation/CRCIS.Web.INoor.CRM.WebApi
RUN dotnet restore

# Copy everything else and build
#COPY . ./
RUN dotnet publish -c Release -o out

ENV http_proxy ''
ENV https_proxy '' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'

# Build runtime image
FROM hub.inoor.ir/common/dotnet/aspnet:5.0-alpine
WORKDIR /app/Presentation/CRCIS.Web.INoor.CRM.WebApi
COPY --from=build-env /app/Presentation/CRCIS.Web.INoor.CRM.WebApi/out ./
EXPOSE 80

ENV http_proxy 'http://172.16.20.207:3128'
ENV https_proxy 'http://172.16.20.207:3128' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'

RUN apk add --no-cache icu-libs
RUN apk add tzdata
ENV TZ="Asia/Tehran" 
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENV http_proxy ''
ENV https_proxy '' 
ENV no_proxy 'localhost,127.0.0.0/8,.local,172.16.0.0/12,192.168.0.0/16,.ir,.noornet.net'
# EXPOSE 5000
# EXPOSE 5001
#ENV ASPNETCORE_URLS=http://+:8011
# ENV ASPNETCORE_HTTPS_PORT=5001
ENTRYPOINT ["dotnet", "CRCIS.Web.INoor.CRM.WebApi.dll"]
