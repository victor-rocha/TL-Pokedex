using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Pokedex.Domain;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Models.Externals;
using Xunit;

namespace Pokedex.UnitTests.Domain
{
    public class BasicPokemonFetcherTests
    {
        [Fact]
        public async Task GetPokemonInfo_FetchesCorrectData()
        {
            // Arrange
            var pokeApiResponseJsonExample = await File.ReadAllTextAsync("Helpers//mew-response-example.json");

            var deserializedPokemon = JsonSerializer.Deserialize<PokemonSpecies>(
                pokeApiResponseJsonExample, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

            var pokemonQueryMock = new Mock<IPokemonQuery>();
            pokemonQueryMock.Setup(q => q.Get(It.IsAny<string>()))
                .ReturnsAsync(deserializedPokemon);

            var basicPokemonFetcher = new BasicPokemonFetcher(pokemonQueryMock.Object);

            // Act
            var basicPokemonInfo = await basicPokemonFetcher.GetPokemonInfo("mew");

            // Assert
            Assert.NotNull(basicPokemonInfo);
            Assert.Equal("mew", basicPokemonInfo.Name);
            Assert.Equal(
                "So rare that it is still said to be a mirage by many experts. Only a few people have seen it worldwide.",
                basicPokemonInfo.Description);
            Assert.Equal("rare", basicPokemonInfo.HabitatName);
            Assert.False(basicPokemonInfo.IsLegendary); // Mew is not Legendary :(((
        }
    }
}