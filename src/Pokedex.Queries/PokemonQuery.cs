using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Interfaces.Externals;
using Pokedex.Queries.Models.Externals;

namespace Pokedex.Queries
{
    public class PokemonQuery : IPokemonQuery
    {
        private readonly IPokeApiClient _pokeApiClient;
        private readonly ILogger<PokemonQuery> _logger;

        public PokemonQuery(IPokeApiClient pokeApiClient, ILogger<PokemonQuery> logger)
        {
            _pokeApiClient = pokeApiClient;
            _logger = logger;
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
            catch (Refit.ApiException exception)
            {
                _logger.LogError(exception,
                    $"An error happened while trying to deserialize the Basic Pokemon Info response with name '{pokemonName}'");
                return null;
            }
            catch (WebException exception)
            {
                _logger.LogError(exception,
                    $"An error happened while getting the Basic Pokemon Info with name '{pokemonName}'");
                return null;
            }
        }
    }
}