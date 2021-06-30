using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Pokedex.Queries.Models.Externals
{
    /// <summary>
    /// Please see: https://pokeapi.co/docs/v2#flavortext
    /// </summary>
    public record FlavorTextResource
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; }

        public NamedAPIResource Language { get; }

        public FlavorTextResource(string flavorText, NamedAPIResource language) 
            => (FlavorText, Language) = (Regex.Replace(flavorText ?? string.Empty, "\n|\f", " "), language);
    }
}