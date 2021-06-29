using System.Threading.Tasks;
using Pokedex.Queries.Models;

namespace Pokedex.Queries.Interfaces
{
    public interface IPokemonTranslationQuery
    {
        Task<string> TranslateDescription(TranslationType translationType, string originalDescription);
    }
}