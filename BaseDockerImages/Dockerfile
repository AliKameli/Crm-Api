FROM hub.inoor.ir/common/dotnet/sdk:6.0 AS build
ARG NUGET
ARG CRCIS_NUGET

#RUN apk add icu-libs

WORKDIR /src
COPY ../ .
WORKDIR /src/Presentation/CRCIS.Web.INoor.CRM.WebApi
RUN dotnet restore --disable-parallel

RUN rm -rf /src
