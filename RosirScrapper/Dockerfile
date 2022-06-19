#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RosirScrapper/RosirScrapper.csproj", "RosirScrapper/"]
RUN dotnet restore "RosirScrapper/RosirScrapper.csproj"
COPY . .
WORKDIR "/src/RosirScrapper"
RUN dotnet build "RosirScrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RosirScrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RosirScrapper.dll"]
