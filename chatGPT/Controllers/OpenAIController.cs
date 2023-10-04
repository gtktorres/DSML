using chatGPT.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> CompleteSentence(string query)
        {
            var result = await _openAIService.CompleteSentence(query);
            return Ok(result);
        }
    }
}
