using OpenAI_API.Embedding;

namespace chatGPT.Services
{
    public interface IOpenAIService
    {
        Task<string> CompleteSentence(string text);
    }
}
