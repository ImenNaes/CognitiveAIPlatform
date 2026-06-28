using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveAIPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CognitiveChatController : ControllerBase
    {
        public readonly IChatCompletionService _chatCompletionService;
        private readonly ILogger<CognitiveChatController> _logger;
        public CognitiveChatController(IChatCompletionService chatCompletionService, ILogger<CognitiveChatController> logger)
        {
            _chatCompletionService = chatCompletionService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ChatCompletionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Le champ 'Prompt' est obligatoire.");

            _logger.LogInformation("Requête reçue avec prompt : {Prompt}", request.Prompt);

            try
            {
                var domainRequest = new ChatCompletionRequest
                {
                    Prompt = request.Prompt
                };

                var response = await _chatCompletionService.GenerateChatAsync(domainRequest);
                _logger.LogInformation("Réponse générée avec succès");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du contenu.");
                return await Task.FromResult<IActionResult>(BadRequest(ex.Message));
            }
        }
    }
}
