using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Services;
using LangSharp.MicrosoftExtensionsDI;
using Microsoft.Extensions.DependencyInjection;

namespace LangSharp.IntegrationTests.Fixtures
{
    public class LangSharpServiceFixture
    {
        public ILangSharpService Service { get; }

        public LangSharpServiceFixture()
        {
            var services = new ServiceCollection();

            var apiKey = Environment.GetEnvironmentVariable("LANGSHARP_TEST_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("LANGSHARP_API_KEY environment variable is not set.");

            var config = new LangSharpConfigurationBuilder()
                .SetAIProvider(AIProviderType.OpenAI)
                .SetModel("gpt-4o-mini")
                .SetApiKey(apiKey)
                .Build();

            services.AddLangSharp(config);

            var provider = services.BuildServiceProvider();
            Service = provider.GetRequiredService<ILangSharpService>();
        }
    }

}
