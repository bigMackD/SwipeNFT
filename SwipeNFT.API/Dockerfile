#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SwipeNFT.API/SwipeNFT.API.csproj", "SwipeNFT.API/"]
COPY ["SwipeNFT.Infrastructure/SwipeNFT.Infrastructure.csproj", "SwipeNFT.Infrastructure/"]
COPY ["SwipeNFT.Contracts/SwipeNFT.Contracts.csproj", "SwipeNFT.Contracts/"]
COPY ["SwipeNFT.Shared.Infrastucture/SwipeNFT.Shared.Infrastructure.csproj", "SwipeNFT.Shared.Infrastucture/"]
COPY ["SwipeNFT.DB/SwipeNFT.DB.csproj", "SwipeNFT.DB/"]
RUN dotnet restore "SwipeNFT.API/SwipeNFT.API.csproj"
COPY . .
WORKDIR "/src/SwipeNFT.API"
RUN dotnet build "SwipeNFT.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwipeNFT.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwipeNFT.API.dll"]