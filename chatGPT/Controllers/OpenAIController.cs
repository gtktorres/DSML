using chatGPT.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatGPT.Controllers
{
    public class OpenAIController : Controller
    {
        private readonly ILogger<OpenAIController> _logger;
        private readonly IOpenAIService _openAIService;

        public OpenAIController(
            ILogger<OpenAIController> logger,
            IOpenAIService openAIService
        )
        {
            _logger = logger;
            _openAIService = openAIService;
        }

        [HttpGet()]
        [Route("CompleteSentence")]
        public async Task<IActionResult> CompleteSentence(string text)
        {
            var result = await _openAIService.CompleteSentence(text);
            return Ok(result);
        }

        [HttpPost()]
        [Route("Embeddings")]
        public async Task<IActionResult> Embeddings(string text)
        {
            var result = await _openAIService.Embeddings(text);
            return Ok(result);
        }
    }
}
