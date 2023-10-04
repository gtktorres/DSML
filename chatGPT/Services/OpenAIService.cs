using Rystem.OpenAi.Chat;
using Rystem.OpenAi;
using chatGPT.Configurations;

namespace chatGPT.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IOpenAiFactory _openAiFactory;
        public OpenAIService(
            IOpenAiFactory openAiFactory
            )
        {
            _openAiFactory = openAiFactory;
        }

        public async Task<string> CompleteSentence(string query)
        {
            //api instance
            var openAiApi = _openAiFactory.CreateChat(null);

            try
            {
                var result = await openAiApi
                    .Request(new ChatMessage { Role = ChatRole.User, Content = $"{query}" })
                    .WithModel(ChatModelType.Gpt35Turbo)
                    .WithTemperature(1)
                    .ExecuteAsync();


                return result.Choices[0].Message.Content;
            }
            catch (HttpRequestException ex)
            {
                var tree = ex;
                return ex.Message;
            }
        }
    }
}
