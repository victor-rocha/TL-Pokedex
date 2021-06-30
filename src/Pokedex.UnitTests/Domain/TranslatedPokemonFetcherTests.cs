using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Pokedex.Domain;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models;
using Pokedex.Queries.Models.Externals;
using Xunit;

namespace Pokedex.UnitTests.Domain
{
    public class TranslatedPokemonFetcherTests
    {
        private readonly Mock<IPokemonQuery> _pokemonQueryMock;
        private readonly Mock<IPokemonTranslationQuery> _pokemonTranslationQueryMock;
        private const string OriginalPokemonDescription = "random-pokemon-desc";

        public TranslatedPokemonFetcherTests()
        {
            _pokemonQueryMock = new Mock<IPokemonQuery>();
            _pokemonTranslationQueryMock = new Mock<IPokemonTranslationQuery>();
        }

        [Fact]
        public async Task LegendaryPokemon_Should_GetDescription_TranslatedToYoda()
        {
            // Arrange
            var pokemonDescription = CreateFlavorTextResourceObject();
            var legendaryFakePokemon = new PokemonSpecies("mewtwo",
                new NamedAPIResource("rare", "https://pokeapi.co/api/v2/pokemon-habitat/5/"), pokemonDescription, true);

            _pokemonQueryMock.Setup(q => q.Get(It.IsAny<string>()))
                .ReturnsAsync(legendaryFakePokemon);

            _pokemonTranslationQueryMock.Setup(t => t.TranslateDescription(TranslationType.Yoda, It.IsAny<string>()))
                .ReturnsAsync("random-yoda-translation-text");

            var translatedPokemonFetcher =
                new TranslatedPokemonFetcher(_pokemonQueryMock.Object, _pokemonTranslationQueryMock.Object);

            // Act
            var translatedPokemon = await translatedPokemonFetcher.GetTranslatedPokemon("mewtwo");

            // Assert
            Assert.NotNull(translatedPokemon);
            Assert.Equal(legendaryFakePokemon.Name, translatedPokemon.Name);
            Assert.Equal(legendaryFakePokemon.IsLegendary, translatedPokemon.IsLegendary);
            Assert.Equal(legendaryFakePokemon.Habitat.Name, translatedPokemon.HabitName);
            Assert.Equal("random-yoda-translation-text", translatedPokemon.Description);

            _pokemonQueryMock.Verify(v => v.Get(It.IsAny<string>()), Times.Once);
            _pokemonTranslationQueryMock.Verify(
                t => t.TranslateDescription(TranslationType.Yoda, OriginalPokemonDescription), Times.Once);
        }

        [Fact]
        public async Task NonLegendaryPokemon_With_HabitatCave_Should_GetDescription_TranslatedToYoda()
        {
            // Arrange
            var pokemonDescription = CreateFlavorTextResourceObject();
            var nonLegendaryFakePokemon = new PokemonSpecies("zubat",
                new NamedAPIResource("cave", "https://pokeapi.co/api/v2/pokemon-habitat/1/"), pokemonDescription,
                false);

            _pokemonQueryMock.Setup(q => q.Get(It.IsAny<string>()))
                .ReturnsAsync(nonLegendaryFakePokemon);

            _pokemonTranslationQueryMock.Setup(t => t.TranslateDescription(TranslationType.Yoda, It.IsAny<string>()))
                .ReturnsAsync("random-yoda-translation-text");

            var translatedPokemonFetcher =
                new TranslatedPokemonFetcher(_pokemonQueryMock.Object, _pokemonTranslationQueryMock.Object);

            // Act
            var translatedPokemon = await translatedPokemonFetcher.GetTranslatedPokemon("zubat");

            // Assert
            Assert.NotNull(translatedPokemon);
            Assert.Equal(nonLegendaryFakePokemon.Name, translatedPokemon.Name);
            Assert.Equal(nonLegendaryFakePokemon.IsLegendary, translatedPokemon.IsLegendary);
            Assert.Equal(nonLegendaryFakePokemon.Habitat.Name, translatedPokemon.HabitName);
            Assert.Equal("random-yoda-translation-text", translatedPokemon.Description);

            _pokemonQueryMock.Verify(v => v.Get(It.IsAny<string>()), Times.Once);
            _pokemonTranslationQueryMock.Verify(
                t => t.TranslateDescription(TranslationType.Yoda, OriginalPokemonDescription), Times.Once);
        }

        [Fact]
        public async Task NonLegendaryPokemon_With_HabitatNotCave_Should_GetDescription_TranslatedToShakespeare()
        {
            // Arrange
            var pokemonDescription = CreateFlavorTextResourceObject();
            var nonLegendaryFakePokemon = new PokemonSpecies("cubone",
                new NamedAPIResource("mountain", "https://pokeapi.co/api/v2/pokemon-habitat/4/"), pokemonDescription,
                false);

            _pokemonQueryMock.Setup(q => q.Get(It.IsAny<string>()))
                .ReturnsAsync(nonLegendaryFakePokemon);

            _pokemonTranslationQueryMock
                .Setup(t => t.TranslateDescription(TranslationType.Shakespeare, It.IsAny<string>()))
                .ReturnsAsync("random-shakespeare-translation-text");

            var translatedPokemonFetcher =
                new TranslatedPokemonFetcher(_pokemonQueryMock.Object, _pokemonTranslationQueryMock.Object);

            // Act
            var translatedPokemon = await translatedPokemonFetcher.GetTranslatedPokemon("cubone");

            // Assert
            Assert.NotNull(translatedPokemon);
            Assert.Equal(nonLegendaryFakePokemon.Name, translatedPokemon.Name);
            Assert.Equal(nonLegendaryFakePokemon.IsLegendary, translatedPokemon.IsLegendary);
            Assert.Equal(nonLegendaryFakePokemon.Habitat.Name, translatedPokemon.HabitName);
            Assert.Equal("random-shakespeare-translation-text", translatedPokemon.Description);

            _pokemonQueryMock.Verify(v => v.Get(It.IsAny<string>()), Times.Once);
            _pokemonTranslationQueryMock.Verify(
                t => t.TranslateDescription(TranslationType.Shakespeare, OriginalPokemonDescription), Times.Once);
        }

        [Fact]
        public async Task FailedTranslation_Should_Return_OriginalDescription()
        {
            // Arrange
            var pokemonDescription = CreateFlavorTextResourceObject();
            var nonLegendaryFakePokemon = new PokemonSpecies("cubone",
                new NamedAPIResource("mountain", "https://pokeapi.co/api/v2/pokemon-habitat/4/"), pokemonDescription,
                false);

            _pokemonQueryMock.Setup(q => q.Get(It.IsAny<string>()))
                .ReturnsAsync(nonLegendaryFakePokemon);

            var funTranslationsApiClientMock = new Mock<IFunTranslationsApiClient>();
            funTranslationsApiClientMock.Setup(f => f.ShakespeareTranslation(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            _pokemonTranslationQueryMock
                .Setup(t => t.TranslateDescription(TranslationType.Shakespeare, It.IsAny<string>()))
                .ReturnsAsync(OriginalPokemonDescription);

            var translatedPokemonFetcher =
                new TranslatedPokemonFetcher(_pokemonQueryMock.Object, _pokemonTranslationQueryMock.Object);

            // Act
            var translatedPokemon = await translatedPokemonFetcher.GetTranslatedPokemon("cubone");

            // Assert
            Assert.NotNull(translatedPokemon);
            Assert.Equal(nonLegendaryFakePokemon.Name, translatedPokemon.Name);
            Assert.Equal(nonLegendaryFakePokemon.IsLegendary, translatedPokemon.IsLegendary);
            Assert.Equal(nonLegendaryFakePokemon.Habitat.Name, translatedPokemon.HabitName);
            Assert.Equal(OriginalPokemonDescription, translatedPokemon.Description);

            _pokemonQueryMock.Verify(v => v.Get(It.IsAny<string>()), Times.Once);
            _pokemonTranslationQueryMock.Verify(
                t => t.TranslateDescription(TranslationType.Shakespeare, OriginalPokemonDescription), Times.Once);
        }

        private static IEnumerable<FlavorTextResource> CreateFlavorTextResourceObject()
        {
            var pokemonDescription = new List<FlavorTextResource>
            {
                new(OriginalPokemonDescription, new NamedAPIResource("en",
                    "https://pokeapi.co/api/v2/language/5/")) // in english
            };
            return pokemonDescription;
        }
    }
}