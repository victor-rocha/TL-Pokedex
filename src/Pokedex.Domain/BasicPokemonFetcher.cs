using System.Linq;
using System.Threading.Tasks;
using Pokedex.Domain.Models;
using Pokedex.Queries.Interfaces;

namespace Pokedex.Domain
{
    public class BasicPokemonFetcher
    {
        private const string DefaultLanguage = "en";
        private readonly IPokemonQuery _pokemonQuery;
        
        public BasicPokemonFetcher(IPokemonQuery pokemonQuery)
        {
            _pokemonQuery = pokemonQuery;
        }

        /// <summary>
        /// Get Pokemon's basic info.
        /// Either from a caching db (TBD) or an external API
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns>Basic pokemon information retrieved</returns>
        public async Task<BasicPokemon> GetPokemonInfo(string pokemonName)
        {
            var pokemonSpecies = await _pokemonQuery.Get(pokemonName);
            if (pokemonSpecies is null)
                return default;

            // Filtering out english description. 
            var defaultEnglishDescription = pokemonSpecies.FlavorTextItems?
                .FirstOrDefault(f => f.Language?.Name == DefaultLanguage)?.FlavorText;

            return new BasicPokemon(pokemonSpecies.Name, pokemonSpecies.Habitat?.Name, defaultEnglishDescription, pokemonSpecies.IsLegendary);
        }
    }
}