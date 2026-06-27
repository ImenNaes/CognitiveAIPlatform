using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChatCompletionService
    {
        Task<string> GenerateChatAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);

    }
}
