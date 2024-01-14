FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WonderFood.WebApi/WonderFood.WebApi.csproj", "src/WonderFood.WebApi/"]
COPY ["src/WonderFood.Core/WonderFood.Core.csproj", "src/WonderFood.Core/"]
COPY ["src/WonderFood.Infra.Sql/WonderFood.Infra.Sql.csproj", "src/WonderFood.Infra.Sql/"]
COPY ["src/WonderFood.UseCases/WonderFood.UseCases.csproj", "src/WonderFood.UseCases/"]
RUN dotnet restore "src/WonderFood.WebApi/WonderFood.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WonderFood.WebApi"
RUN dotnet build "WonderFood.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WonderFood.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final12345d
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WonderFood.WebApi.dll"]
