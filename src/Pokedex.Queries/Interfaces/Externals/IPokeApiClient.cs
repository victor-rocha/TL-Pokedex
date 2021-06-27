using Refit;
using System.Threading.Tasks;
using Pokedex.Queries.Models.Externals;

namespace Pokedex.Queries.Interfaces.Externals
{
    public interface IPokeApiClient
    {
        /// <summary>
        /// Please see: https://pokeapi.co/docs/v2#pokemon-species
        /// The endpoint can take both an Id or Name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        [Get("/v2/pokemon-species/{pokemonName}")]
        Task<PokemonSpecies> GetSpecie(string pokemonName);
    }
}