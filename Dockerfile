﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pokedex/Pokedex.csproj", "Pokedex/"]
RUN dotnet restore "Pokedex/Pokedex.csproj"
COPY . .
WORKDIR "/src/Pokedex"
RUN dotnet build "Pokedex.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokedex.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokedex.dll"]
