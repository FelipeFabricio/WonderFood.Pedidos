FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WonderFood.Worker/WonderFood.Worker.csproj", "src/WonderFood.Worker/"]
COPY ["src/WonderFood.Domain/WonderFood.Domain.csproj", "src/WonderFood.Domain/"]
COPY ["src/WonderFood.Infra.Sql/WonderFood.Infra.Sql.csproj", "src/WonderFood.Infra.Sql/"]
COPY ["src/WonderFood.Application/WonderFood.Application.csproj", "src/WonderFood.Application/"]
COPY ["src/WonderFood.Models/WonderFood.Models.csproj", "src/WonderFood.Models/"]
RUN dotnet restore "src/WonderFood.Worker/WonderFood.Worker.csproj"
COPY . .
WORKDIR "/src/src/WonderFood.Worker"
RUN dotnet build "WonderFood.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WonderFood.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WonderFood.Worker.dll"]
