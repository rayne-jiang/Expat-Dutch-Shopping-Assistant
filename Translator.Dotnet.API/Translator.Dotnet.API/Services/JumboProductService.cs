using RestSharp;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Translator.Dotnet.API.Services
{
    public class ProductInfo
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;

        public ProductInfo()
        {
            ImageUrl = string.Empty;
            ProductName = string.Empty;
        }
    }

    public interface IJumboProductService
    {
        Task<List<ProductInfo>> SearchProductsAsync(string searchTerm);
    }

    public class JumboProductService : IJumboProductService
    {
        private readonly RestClient _client;
        private const int DEFAULT_TIMEOUT_MS = 30000; // 30 seconds

        public JumboProductService()
        {
            var options = new RestClientOptions("https://www.jumbo.com")
            {
                Timeout = TimeSpan.FromMilliseconds(DEFAULT_TIMEOUT_MS)
            };
            _client = new RestClient(options);
        }

        public async Task<List<ProductInfo>> SearchProductsAsync(string searchTerm)
        {
            var request = new RestRequest("producten/", Method.Get);
            request.AddParameter("searchType", "keyword");
            request.AddParameter("searchTerms", searchTerm);

            var response = await _client.ExecuteAsync(request);
            var products = new List<ProductInfo>();

            if (!string.IsNullOrEmpty(response.Content))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response.Content);

                var productImageNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='product-image']");
                
                if (productImageNodes != null)
                {
                    foreach (var node in productImageNodes)
                    {
                        var imgNode = node.SelectSingleNode(".//img");
                        if (imgNode != null)
                        {
                            var product = new ProductInfo
                            {
                                ImageUrl = imgNode.GetAttributeValue("src", ""),
                                ProductName = imgNode.GetAttributeValue("alt", "")
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
} 