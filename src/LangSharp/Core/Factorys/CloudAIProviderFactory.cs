using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Providers;

namespace LangSharp.Core.Factorys
{
    public static class CloudAIProviderFactory
    {
        public static ICloudAIProvider CreateProvider(AIProviderType providerType)
        {
            return providerType switch
            {
                AIProviderType.GoogleCloud => new GoogleCloudAIProvider(),
                AIProviderType.OpenAI => new OpenAIProvider(),
                _ => throw new ArgumentException("Unknown AI provider", nameof(providerType)),
            };
        }
    }
}
