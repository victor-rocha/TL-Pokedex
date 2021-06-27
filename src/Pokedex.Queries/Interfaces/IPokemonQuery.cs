using System.Threading.Tasks;
using Pokedex.Queries.Models.Externals;

namespace Pokedex.Queries.Interfaces
{
    public interface IPokemonQuery
    {
        Task<PokemonSpecies> Get(string pokemonName);
    }
}