FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY Pokedex.sln .
COPY src/Pokedex.API/Pokedex.API.csproj src/Pokedex.API/
COPY src/Pokedex.Domain/Pokedex.Domain.csproj src/Pokedex.Domain/
COPY src/Pokedex.Queries/Pokedex.Queries.csproj src/Pokedex.Queries/
COPY src/Pokedex.UnitTests/Pokedex.UnitTests.csproj src/Pokedex.UnitTests/
RUN dotnet restore Pokedex.sln
COPY . .
RUN dotnet build -c Release --no-restore && \
  dotnet publish src/Pokedex.API/Pokedex.API.csproj -c Release -o /build/publish --no-restore --no-build

FROM base AS final
WORKDIR /app
COPY --from=build /build/publish .
ENTRYPOINT ["dotnet", "Pokedex.API.dll"]
