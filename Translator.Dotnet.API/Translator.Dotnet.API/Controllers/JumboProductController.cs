using Microsoft.AspNetCore.Mvc;
using Translator.Dotnet.API.Services;

namespace Translator.Dotnet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JumboProductController : ControllerBase
    {
        private readonly IJumboProductService _jumboProductService;

        public JumboProductController(IJumboProductService jumboProductService)
        {
            _jumboProductService = jumboProductService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty");
            }

            var result = await _jumboProductService.SearchProductsAsync(searchTerm);
            return Ok(result);
        }
    }
} 