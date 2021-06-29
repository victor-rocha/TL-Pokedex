namespace Pokedex.Domain.Models
{
    public record TranslatedPokemon
    {
        public string Name { get; }
        public string Description { get; }
        public string HabitName { get; }
        public bool IsLegendary { get; }
        
        public TranslatedPokemon(string name, string description, string habitName, bool isLegendary)
            => (Name, Description, HabitName, IsLegendary) = (name, description, habitName, isLegendary);
    }
}