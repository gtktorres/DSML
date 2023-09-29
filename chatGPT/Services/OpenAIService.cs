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
    
        public async Task<string> CompleteSentence(string text)
        {
            //api instance
            var api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);
            var result = await api.Completions.GetCompletion(text);
            return result;
        }

        public async Task<EmbeddingResult> Embeddings(string text)
        {
            //api instance
            var embeddingRequest = new EmbeddingRequest();
            embeddingRequest.Input = text;
            embeddingRequest.Model = OpenAI_API.Models.Model.AdaTextEmbedding;

            var api = new OpenAI_API.OpenAIAPI( _openAIConfig.Key);
            var result = await api.Embeddings.CreateEmbeddingAsync(text);
            return result;
        }
    }
}
