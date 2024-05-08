FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WonderFood.Worker/WonderFood.Worker.csproj", "src/WonderFood.Worker/"]
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
