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
        public async Task<IActionResult> CompleteSentence(string query)
        {
            string OutPutResult = "";
            var completionRequest = new CompletionRequest();
            completionRequest.Prompt = query;
            completionRequest.Model = OpenAI_API.Models.Model.AdaTextEmbedding;

            var completions = openai.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Result.Completions)
            {
                OutPutResult += completion.Text;
            }

            return Ok(completions);
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
