namespace Pokedex.Domain.Models
{
    public record BasicPokemon
    {
        public string Name { get; }
        public string HabitatName { get; }
        public string Description { get; }
        public bool IsLegendary { get; }
        
        public BasicPokemon(string name, string habitatName, string description, bool isLegendary)
            => (Name, HabitatName, Description, IsLegendary) = (name, habitatName ?? string.Empty, description ?? string.Empty, isLegendary);
    }
}