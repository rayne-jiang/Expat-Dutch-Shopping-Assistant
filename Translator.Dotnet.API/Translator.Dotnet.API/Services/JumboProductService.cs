using RestSharp;

namespace Translator.Dotnet.API.Services
{
    public interface IJumboProductService
    {
        Task<string> SearchProductsAsync(string searchTerm);
    }

    public class JumboProductService : IJumboProductService
    {
        private readonly RestClient _client;

        public JumboProductService()
        {
            _client = new RestClient("https://www.jumbo.com");
            _client.Timeout = -1;
        }

        public async Task<string> SearchProductsAsync(string searchTerm)
        {
            var request = new RestRequest("producten/", Method.Get);
            request.AddParameter("searchType", "keyword");
            request.AddParameter("searchTerms", searchTerm);

            var response = await _client.ExecuteAsync(request);
            return response.Content ?? string.Empty;
        }
    }
} 