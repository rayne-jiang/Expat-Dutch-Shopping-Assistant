using System;
using System.Text.Json;
using System.Threading.Tasks;
using Translator.Dotnet.API.Models;
using RestSharp;

namespace Translator.Dotnet.API.Service
{
    public class TranslatorService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private IRestClient _restClient;

        public TranslatorService()
        {
            _apiKey = Environment.GetEnvironmentVariable("DEEPL_API_KEY") 
                ?? throw new ArgumentNullException("DEEPL_API_KEY is not set");
            
            _baseUrl = Environment.GetEnvironmentVariable("DEEPL_BASE_URL") 
                ?? "https://api-free.deepl.com/v2/translate";

            _restClient = new RestClient(_baseUrl);
        }

        public async Task<string> TranslateTextAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be empty", nameof(text));

            if (string.IsNullOrWhiteSpace(targetLanguage))
                throw new ArgumentException("Target language cannot be empty", nameof(targetLanguage));

            var request = new RestRequest()
                .AddJsonBody(new
                {
                    text = new[] { text },
                    target_lang = targetLanguage.ToUpper()
                });

            request.AddHeader("Authorization", $"DeepL-Auth-Key {_apiKey}");
            request.AddHeader("Content-Type", "application/json");

            var response = await _restClient.PostAsync(request);

            if (!response.IsSuccessful)
                throw new Exception($"Translation failed: {response.ErrorMessage}");

            var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(response.Content);
            return translationResponse?.translations?[0]?.text 
                ?? throw new Exception("Failed to parse translation response");
        }
    }
}
