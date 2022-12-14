#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket/SAF.Basket.Api/SAF.Basket.Api.csproj", "Services/Basket/SAF.Basket.Api/"]
COPY ["BuildingBlocks/SAF.EventBus.Messages/SAF.EventBus.Messages.csproj", "BuildingBlocks/SAF.EventBus.Messages/"]
RUN dotnet restore "Services/Basket/SAF.Basket.Api/SAF.Basket.Api.csproj"
COPY . .
WORKDIR "/src/Services/Basket/SAF.Basket.Api"
RUN dotnet build "SAF.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SAF.Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SAF.Basket.Api.dll"]