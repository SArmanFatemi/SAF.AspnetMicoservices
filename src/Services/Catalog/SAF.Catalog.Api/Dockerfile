#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/SAF.Catalog.Api/SAF.Catalog.Api.csproj", "Services/Catalog/SAF.Catalog.Api/"]
RUN dotnet restore "Services/Catalog/SAF.Catalog.Api/SAF.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/SAF.Catalog.Api"
RUN dotnet build "SAF.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SAF.Catalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SAF.Catalog.Api.dll"]