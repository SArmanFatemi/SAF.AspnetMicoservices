#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Ordering/SAF.Ordering.Api/SAF.Ordering.Api.csproj", "Services/Ordering/SAF.Ordering.Api/"]
COPY ["BuildingBlocks/SAF.EventBus.Messages/SAF.EventBus.Messages.csproj", "BuildingBlocks/SAF.EventBus.Messages/"]
COPY ["Services/Ordering/SAF.Ordering.Application/SAF.Ordering.Application.csproj", "Services/Ordering/SAF.Ordering.Application/"]
COPY ["Services/Ordering/SAF.Ordering.Domain/SAF.Ordering.Domain.csproj", "Services/Ordering/SAF.Ordering.Domain/"]
COPY ["Services/Ordering/SAF.Ordering.Infrastructure/SAF.Ordering.Infrastructure.csproj", "Services/Ordering/SAF.Ordering.Infrastructure/"]
RUN dotnet restore "Services/Ordering/SAF.Ordering.Api/SAF.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/SAF.Ordering.Api"
RUN dotnet build "SAF.Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SAF.Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SAF.Ordering.Api.dll"]