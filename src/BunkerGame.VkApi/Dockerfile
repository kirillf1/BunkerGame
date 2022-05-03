#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BunkerGame.VkApi/BunkerGame.VkApi.csproj", "src/BunkerGame.VkApi/"]
COPY ["src/BunkerGame.Infrastructure/BunkerGame.Infrastructure.csproj", "src/BunkerGame.Infrastructure/"]
COPY ["src/BunkerGame.Domain/BunkerGame.Domain.csproj", "src/BunkerGame.Domain/"]
COPY ["src/BunkerGame.Application/BunkerGame.Application.csproj", "src/BunkerGame.Application/"]
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
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet BunkerGame.VkApi.dll