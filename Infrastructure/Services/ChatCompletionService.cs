using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class ChatCompletionService : IChatCompletionService
    {
        private readonly AIModelProviderSettings _options;
        private readonly AzureOpenAIClient _client;
        private readonly ILogger<ChatCompletionService> _logger;
        public ChatCompletionService(IOptions<AIModelProviderSettings> options, ILogger<ChatCompletionService> logger)
        {
            _options = options.Value;
            _logger = logger;

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
                var messages = new List<ChatMessage> { new UserChatMessage(request.Prompt) };

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
                _logger.LogInformation("Réponse générée : {Response}", chatResponse);
                return chatResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur dans GenerateChatAsync");
                throw;
            }

        }
    }
}
