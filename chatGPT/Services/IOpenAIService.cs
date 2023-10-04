using Rystem.OpenAi.Chat;

namespace chatGPT.Services
{
    public interface IOpenAIService
    {
        Task<string> CompleteSentence(string query);
    }
}
