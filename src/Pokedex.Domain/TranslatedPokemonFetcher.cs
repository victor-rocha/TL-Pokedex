using System.Threading.Tasks;
using Pokedex.Domain.Models;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Models;

namespace Pokedex.Domain
{
    public class TranslatedPokemonFetcher
    {
        private const string YodaHabitatName = "cave";
        private readonly BasicPokemonFetcher _basicPokemonFetcher;
        private readonly IPokemonTranslationQuery _pokemonTranslationQuery;

        public TranslatedPokemonFetcher(IPokemonQuery pokemonQuery, IPokemonTranslationQuery pokemonTranslationQuery)
        {
            _basicPokemonFetcher = new BasicPokemonFetcher(pokemonQuery);
            _pokemonTranslationQuery = pokemonTranslationQuery;
        }

        public async Task<TranslatedPokemon> GetTranslatedPokemon(string pokemonName)
        {
            var basicPokemon = await _basicPokemonFetcher.GetPokemonInfo(pokemonName);

            if (basicPokemon is null)
                return default;

            var translatedDescription =
                await _pokemonTranslationQuery.TranslateDescription(GetTranslationType(basicPokemon), basicPokemon.Description);

            return new TranslatedPokemon(basicPokemon.Name, translatedDescription ?? basicPokemon.Description,
                basicPokemon.HabitatName, basicPokemon.IsLegendary);
        }

        /// <summary>
        /// Returns the translation type based on pokemon's habitat or Legendary
        /// </summary>
        /// <param name="basicPokemonInfo">Pokemon to be checked against</param>
        /// <returns></returns>
        private static TranslationType GetTranslationType(BasicPokemon basicPokemonInfo)
        {
            if (basicPokemonInfo.HabitatName == YodaHabitatName || basicPokemonInfo.IsLegendary)
                return TranslationType.Yoda;

            return TranslationType.Shakespeare;
        }
    }
}