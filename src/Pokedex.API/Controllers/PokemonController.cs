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
        public PokemonController(BasicPokemonFetcher basicPokemonFetcher)
        {
            _basicPokemonFetcher = basicPokemonFetcher;
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
            
            if (basicPokemonInfo == null)
                return NotFound();
            
            return Ok(basicPokemonInfo);
        }

        /// <summary>
        /// Retrieves a translated information regarding the given Pokemon.
        /// </summary>
        /// <param name="pokemonName">Pokemon's name</param>
        /// <returns></returns>
        [HttpGet("{pokemonName}/translated")]
        public async Task<IActionResult> GetTranslated([FromRoute] string pokemonName)
        {
            return null;
        }
    }
}