using Microsoft.AspNetCore.Mvc;
using Translator.Dotnet.API.Services;

namespace Translator.Dotnet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JumboProductController : ControllerBase
    {
        private readonly IJumboProductService _jumboProductService;
        private readonly TranslatorService _translatorService;

        public JumboProductController(IJumboProductService jumboProductService, TranslatorService translatorService)
        {
            _jumboProductService = jumboProductService;
            _translatorService = translatorService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty");
            }

            // Translate search term to Dutch
            var translatedSearchTerm = await _translatorService.TranslateTextAsync(searchTerm, "NL");

            var productImages = await _jumboProductService.SearchProductsAsync(translatedSearchTerm);
            return Ok(new { productImages });
        }
    }
} 