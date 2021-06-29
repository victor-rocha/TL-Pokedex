using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models;

namespace Pokedex.Queries
{
    public class PokemonTranslationQuery : IPokemonTranslationQuery
    {
        private readonly IFunTranslationsApiClient _funTranslationsApiClient;
        private readonly ILogger<PokemonTranslationQuery> _logger;

        public PokemonTranslationQuery(IFunTranslationsApiClient funTranslationsApiClient,
            ILogger<PokemonTranslationQuery> logger)
        {
            _funTranslationsApiClient = funTranslationsApiClient;
            _logger = logger;
        }

        public async Task<string> TranslateDescription(TranslationType translationType, string originalDescription)
        {
            try
            {
                // we could check within the cache via using the translationType + originalDescription for example

                var response = translationType switch
                {
                    TranslationType.Yoda => await _funTranslationsApiClient.YodaTranslation(originalDescription),
                    _ => await _funTranslationsApiClient.ShakespeareTranslation(originalDescription),
                };

                // Here we would add the translation to a cache.

                return response?.Contents?.Translated;
            }
            // We could possibly catch Refit.ApiException too.
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"An error happened while translating '{originalDescription}' to the type {translationType}");
                return default;
            }
        }
    }
}