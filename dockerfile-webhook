FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WonderFood.WebHook/WonderFood.WebHook.csproj", "src/WonderFood.WebHook/"]
RUN dotnet restore "src/WonderFood.WebHook/WonderFood.WebHook.csproj"
COPY . .
WORKDIR "/src/src/WonderFood.WebHook"
RUN dotnet build "WonderFood.WebHook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WonderFood.WebHook.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WonderFood.WebHook.dll"]
