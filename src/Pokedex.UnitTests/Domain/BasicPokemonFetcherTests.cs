using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Pokedex.Queries;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models.Externals;
using Xunit;

namespace Pokedex.UnitTests.Domain
{
    public class BasicPokemonFetcherTests
    {
        [Fact]
        public async Task PokemonQuery_Get_Works()
        {
            // Arrange
            var pokemonSpecies = new PokemonSpecies("", new NamedAPIResource("", ""), new List<FlavorTextResource>(), true);
            var pokeApiClientMock = new Mock<IPokeApiClient>();
            pokeApiClientMock.Setup(c => c.GetSpecie(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpecies);

            var pokemonQuery = new PokemonQuery(pokeApiClientMock.Object, NullLogger<PokemonQuery>.Instance);

            // Act
            var result = await pokemonQuery.Get("mew");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pokemonSpecies, result);
            pokeApiClientMock.Verify(c => c.GetSpecie("mew"), Times.Once);
        }

        [Fact]
        public async Task PokemonQuery_Get_ReturnsNull_When_ExceptionIsThrown()
        {
            // Arrange
            var pokeApiClientMock = new Mock<IPokeApiClient>();
            pokeApiClientMock.Setup(c => c.GetSpecie(It.IsAny<string>()))
                .ThrowsAsync(new WebException());

            var query = new PokemonQuery(pokeApiClientMock.Object, NullLogger<PokemonQuery>.Instance);

            // Act
            var result = await query.Get("mew");

            // Assert
            Assert.Null(result);
            pokeApiClientMock.Verify(c => c.GetSpecie("mew"), Times.Once);
        }
    }
}