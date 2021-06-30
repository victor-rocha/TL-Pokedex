using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models.Externals;
using Xunit;
using Moq;
using Pokedex.Queries;
using Pokedex.Queries.Models;

namespace Pokedex.UnitTests.Queries
{
    public class PokemonTranslationQueryTests
    {
        private readonly Mock<IFunTranslationsApiClient> _funTranslationsApiClientMock;
        private const string OriginalDescription = "An original description";
        private const string TranslatedDescription = "A pretty good translated description";

        public PokemonTranslationQueryTests()
        {
            _funTranslationsApiClientMock = new Mock<IFunTranslationsApiClient>();
        }

        [Theory]
        [InlineData(TranslationType.Yoda)]
        [InlineData(TranslationType.Shakespeare)]
        public async Task TranslateDescription_ReturnsTranslatedDescription(TranslationType translationType)
        {
            if (translationType == TranslationType.Yoda)
            {
                _funTranslationsApiClientMock.Setup(s =>
                        s.YodaTranslation(It.IsAny<string>()))
                    .ReturnsAsync(
                        new FunTranslationResponse(new FunTranslationResponseContents(TranslatedDescription)));
            }
            else
            {
                _funTranslationsApiClientMock.Setup(s =>
                        s.ShakespeareTranslation(It.IsAny<string>()))
                    .ReturnsAsync(
                        new FunTranslationResponse(new FunTranslationResponseContents(TranslatedDescription)));
            }

            var pokemonTranslationQuery = new PokemonTranslationQuery(_funTranslationsApiClientMock.Object,
                NullLogger<PokemonTranslationQuery>.Instance);

            // Act
            var translatedDescription =
                await pokemonTranslationQuery.TranslateDescription(translationType, OriginalDescription);

            // Assert
            Assert.NotNull(translatedDescription);
            Assert.NotEmpty(translatedDescription);
            Assert.Equal(TranslatedDescription, translatedDescription);
        }

        [Fact]
        public async Task TranslateDescription_ReturnsNullIfAnErrorOcurred()
        {
            _funTranslationsApiClientMock.Setup(s =>
                    s.ShakespeareTranslation(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var pokemonTranslationQuery = new PokemonTranslationQuery(_funTranslationsApiClientMock.Object,
                NullLogger<PokemonTranslationQuery>.Instance);

            // Act
            var translatedDescription =
                await pokemonTranslationQuery.TranslateDescription(TranslationType.Shakespeare, OriginalDescription);

            // Assert
            Assert.Null(translatedDescription);
        }

        [Fact]
        public async Task TranslateDescription_ReturnsNullIfNoTranslationAvailable()
        {
            _funTranslationsApiClientMock.Setup(s =>
                    s.ShakespeareTranslation(It.IsAny<string>()))
                .ReturnsAsync(
                    new FunTranslationResponse(null));

            var pokemonTranslationQuery = new PokemonTranslationQuery(_funTranslationsApiClientMock.Object,
                NullLogger<PokemonTranslationQuery>.Instance);

            // Act
            var translatedDescription =
                await pokemonTranslationQuery.TranslateDescription(TranslationType.Shakespeare, OriginalDescription);

            // Assert
            Assert.Null(translatedDescription);
        }
    }
}