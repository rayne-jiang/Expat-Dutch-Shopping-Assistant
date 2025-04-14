namespace Translator.Dotnet.API.Models
{
    public record TranslationResponse
    {
        public required Translation[] translations { get; init; }
    }

    public record Translation
    {
        public required string text { get; init; }
        public string detected_source_language { get; init;}
    }
} 