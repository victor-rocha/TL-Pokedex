using System.Threading.Tasks;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models.Externals;

namespace Pokedex.Queries
{
    public class PokemonQuery : IPokemonQuery
    {
        private readonly IPokeApiClient _pokeApiClient;

        public PokemonQuery(IPokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }
        
        /// <summary>
        /// Get the info for a given Pokemon.
        /// In this case, using PokeAPI (or a cache) and its objects
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public async Task<PokemonSpecies> Get(string pokemonName)
        {
            try
            {
                // Before we search for the pokemon using PokeApi, we could search in the cache
                // i.e await _cache.TryGet(pokemonName);
                var pokemonSpecie = await _pokeApiClient.GetSpecie(pokemonName);
                // We would add to the cache after the response was sucessfull. 
                // i.e await _cache.TrySet(pokemonName, pokemonSpecie);
                
                return pokemonSpecie;
            }
            catch (Refit.ApiException)
            {
                return null;
            }
        }
    }
}