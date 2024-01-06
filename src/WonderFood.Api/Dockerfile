FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WonderFood.Api/WonderFood.Api.csproj", "src/WonderFood.Api/"]
RUN dotnet restore "src/WonderFood.Api/WonderFood.Api.csproj"
COPY . .
WORKDIR "/src/src/WonderFood.Api"
RUN dotnet build "WonderFood.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WonderFood.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WonderFood.Api.dll"]
