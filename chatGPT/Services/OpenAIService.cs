using chatGPT.Configurations;
using Microsoft.Extensions.Options;
using OpenAI_API.Embedding;

namespace chatGPT.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAIConfig _openAIConfig;
        public OpenAIService(
            IOptionsMonitor<OpenAIConfig> optionsMonitor
        )
        {
            _openAIConfig = optionsMonitor.CurrentValue;
        }
    
        public async Task<string> CompleteSentence(string query)
        {
            //api instance
            var api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);
            var result = await api.Completions.GetCompletion(query);
            return result;
        }
    }
}
