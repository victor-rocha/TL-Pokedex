using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Domain;
using Pokedex.Domain.Models;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PokemonController : ControllerBase
    {
        private readonly BasicPokemonFetcher _basicPokemonFetcher;
        private readonly TranslatedPokemonFetcher _translatedPokemonFetcher;

        public PokemonController(BasicPokemonFetcher basicPokemonFetcher,
            TranslatedPokemonFetcher translatedPokemonFetcher)
        {
            _basicPokemonFetcher = basicPokemonFetcher;
            _translatedPokemonFetcher = translatedPokemonFetcher;
        }

        /// <summary>
        /// Retrieves basic Pokemon's information. 
        /// </summary>
        /// <param name="pokemonName">Pokemon's name</param>
        /// <returns></returns>
        [HttpGet("{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicPokemon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] string pokemonName)
        {
            var basicPokemonInfo = await _basicPokemonFetcher.GetPokemonInfo(pokemonName.ToLower());

            if (basicPokemonInfo is null)
                return NotFound();

            return Ok(basicPokemonInfo);
        }

        /// <summary>
        /// Retrieves a translated information regarding the given Pokemon.
        /// </summary>
        /// <param name="pokemonName">Pokemon's name</param>
        /// <returns></returns>
        [HttpGet("translated/{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslatedPokemon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTranslated([FromRoute] string pokemonName)
        {
            var translatedPokemon = await _translatedPokemonFetcher.GetTranslatedPokemon(pokemonName.ToLower());

            if (translatedPokemon is null)
                return NotFound();

            return Ok(translatedPokemon);
        }
    }
}