#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BunkerGame.VkApi/BunkerGame.VkApi.csproj", "src/BunkerGame.VkApi/"]
COPY ["src/BunkerGameComponents.Infrastructure/BunkerGameComponents.Infrastructure.csproj", "src/BunkerGameComponents.Infrastructure/"]
COPY ["src/BunkerGame.Framework/BunkerGame.Framework.csproj", "src/BunkerGame.Framework/"]
COPY ["src/BunkerGameComponents.Domain/BunkerGameComponents.Domain.csproj", "src/BunkerGameComponents.Domain/"]
COPY ["src/BunkerGame.GameTypes/BunkerGame.GameTypes.csproj", "src/BunkerGame.GameTypes/"]
COPY ["src/BunkerGame.Domain/BunkerGame.Domain.csproj", "src/BunkerGame.Domain/"]
RUN dotnet restore "src/BunkerGame.VkApi/BunkerGame.VkApi.csproj"
COPY . .
WORKDIR "/src/src/BunkerGame.VkApi"
RUN dotnet build "BunkerGame.VkApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BunkerGame.VkApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "BunkerGame.VkApi.dll", "--environment=Production"]