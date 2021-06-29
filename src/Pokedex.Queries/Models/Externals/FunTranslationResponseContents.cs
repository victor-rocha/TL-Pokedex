namespace Pokedex.Queries.Models.Externals
{
    public record FunTranslationResponseContents
    {
        public string Translated { get; }

        public FunTranslationResponseContents(string translated)
            => (Translated) = (translated); 
    }
}