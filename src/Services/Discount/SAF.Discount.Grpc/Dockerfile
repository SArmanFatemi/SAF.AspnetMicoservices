#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/SAF.Discount.Grpc/SAF.Discount.Grpc.csproj", "Services/Discount/SAF.Discount.Grpc/"]
RUN dotnet restore "Services/Discount/SAF.Discount.Grpc/SAF.Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/SAF.Discount.Grpc"
RUN dotnet build "SAF.Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SAF.Discount.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SAF.Discount.Grpc.dll"]