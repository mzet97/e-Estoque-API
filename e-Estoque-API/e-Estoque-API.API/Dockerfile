#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["e-Estoque-API.API/e-Estoque-API.API.csproj", "e-Estoque-API.API/"]
COPY ["e-Estoque-API.Application/e-Estoque-API.Application.csproj", "e-Estoque-API.Application/"]
COPY ["e-Estoque-API.Core/e-Estoque-API.Core.csproj", "e-Estoque-API.Core/"]
COPY ["e-Estoque-API.Infrastructure/e-Estoque-API.Infrastructure.csproj", "e-Estoque-API.Infrastructure/"]
RUN dotnet restore "./e-Estoque-API.API/./e-Estoque-API.API.csproj"
COPY . .
WORKDIR "/src/e-Estoque-API.API"
RUN dotnet build "./e-Estoque-API.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./e-Estoque-API.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-Estoque-API.API.dll"]