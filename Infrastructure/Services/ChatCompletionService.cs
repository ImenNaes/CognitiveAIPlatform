using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace Infrastructure.Services
{
    public class ChatCompletionService : IChatCompletionService
    {
        private readonly AIModelProviderSettings _options;
        private readonly AzureOpenAIClient _client;
        public ChatCompletionService(IOptions<AIModelProviderSettings> options)
        {
            _options = options.Value;
            _client = new AzureOpenAIClient(
                   new Uri(_options.AzureOpenAIEndpoint!),
                   new AzureKeyCredential(_options.AzureOpenAIApiKey!)
            );
        }

        public async Task<string> GenerateChatAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var chatClient = _client.GetChatClient(_options.DeploymentModelName);

                var messages = new List<ChatMessage>
                {
                    new UserChatMessage(request.Prompt)
                };

                var response = await chatClient.CompleteChatAsync(
                    messages,
                    new ChatCompletionOptions
                    {
                        Temperature = 0.7f,
                        FrequencyPenalty = 0f,
                        PresencePenalty = 0f
                    }
                );

                var chatResponse = response.Value.Content.Last().Text;
                return chatResponse;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
