using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace chatGPT.Controllers
{
    [ApiController]
    public class ChatGPTController : Controller
    {

        [HttpGet]
        [Route("UseChatGpt")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            string OutPutResult = "";
            var openai = new OpenAIAPI("k-hyI4S5pr5knqZT82JGeHT3BlbkFJemmGSDCq8RrstaFWfnmJ");
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
    
    
    
    
    }
}
