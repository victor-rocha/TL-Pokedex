namespace Pokedex.Queries.Models.Externals
{
    public record FunTranslationResponse
    {
        public FunTranslationResponseContents Contents { get;  }

        public FunTranslationResponse(FunTranslationResponseContents contents)
            => (Contents) = (contents);
    }
}