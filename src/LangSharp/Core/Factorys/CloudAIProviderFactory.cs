using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Providers;

namespace LangSharp.Core.Factorys
{
    public static class CloudAIProviderFactory
    {
        public static ICloudAIProvider CreateProvider(AIProviderType providerType, IPythonService pythonService)
        {
            return providerType switch
            {
                AIProviderType.GoogleCloud => new GoogleCloudAIProvider(),
                AIProviderType.LangChain => new LangChainProvider(pythonService),
                _ => throw new ArgumentException("Unknown AI provider", nameof(providerType)),
            };
        }
    }
}
