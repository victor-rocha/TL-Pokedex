using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Queries.Models.Externals
{
    /// <summary>
    /// Please see: https://pokeapi.co/docs/v2#pokemon-species
    /// </summary>
    public record PokemonSpecies
    {
        public string Name { get; }
        public NamedAPIResource Habitat { get; }
        
        [JsonPropertyName("flavor_text_entries")]
        public IEnumerable<FlavorTextResource> FlavorTextItems { get; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; }
        
        public PokemonSpecies(string name, NamedAPIResource habitat, 
            IEnumerable<FlavorTextResource> flavorTextItems, bool isLegendary)
            => (Name, Habitat, FlavorTextItems, IsLegendary) 
                = (name, habitat, flavorTextItems, isLegendary);
    }
}