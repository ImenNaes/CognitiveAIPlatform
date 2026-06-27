namespace Infrastructure.Configs
{
    public class AIModelProviderSettings
    {
        public string? AzureOpenAIEndpoint { get; set; }
        public string? AzureOpenAIApiKey { get; set; }
        public string? DeploymentModelName { get; set; }
    }
}
