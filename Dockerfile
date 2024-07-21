﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Web/Web.csproj Web/
COPY Web.Client/Web.Client.csproj WebClient/
COPY Shared/Shared.csproj Shared/
COPY ProjectExamination.ServiceDefaults/ProjectExamination.ServiceDefaults.csproj ServiceDefault/
RUN dotnet restore Web/Web.csproj

COPY . .
WORKDIR /src/Web
RUN dotnet build "Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN ln -sf /usr/share/zoneinfo/posix/Asia/Jakarta /etc/localtime
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
