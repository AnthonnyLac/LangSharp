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
                AIProviderType.GoogleCloud => throw new NotImplementedException("Google Cloud AI provider will be available in a future release."),
                AIProviderType.LangChain => new LangChainProvider(pythonService),
                _ => throw new ArgumentException("Unknown AI provider", nameof(providerType)),
            };
        }
    }
}
