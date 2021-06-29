using System.Threading.Tasks;
using Pokedex.Queries.Models.Externals;
using Refit;

namespace Pokedex.Queries.Interfaces.Externals
{
    public interface IFunTranslationsApiClient
    {
        [Multipart]
        [Post("/translate/yoda.json")]
        Task<FunTranslationResponse> YodaTranslation(string text);

        [Multipart]
        [Post("/translate/shakespeare.json")]
        Task<FunTranslationResponse> ShakespeareTranslation(string text);
    }
}