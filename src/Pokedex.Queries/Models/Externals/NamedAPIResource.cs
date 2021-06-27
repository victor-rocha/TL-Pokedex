namespace Pokedex.Queries.Models.Externals
{
    /// <summary>
    /// Please see: https://pokeapi.co/docs/v2#namedapiresource
    /// </summary>
    public record NamedAPIResource
    {
        public string Name { get; }
        public string Url { get; }

        public NamedAPIResource(string name, string url) => (Name, Url) = (name, url);
    }
}