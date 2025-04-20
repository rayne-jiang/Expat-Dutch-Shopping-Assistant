namespace Translator.Dotnet.API.Models
{
    public class TranslationResponse
    {
        public List<Translation> translations { get; set; } = new();
    }

    public class Translation
    {
        public string detected_source_language { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
    }
} 