using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;

namespace LangSharp.UnitTests.Core.Configuration
{
    public class LangSharpConfigurationBuilderTests
    {
        [Fact]
        public void Build_ShouldReturnConfiguredLangSharpConfiguration()
        {
            // Arrange
            var builder = new LangSharpConfigurationBuilder();

            // Act
            var configuration = builder
                .SetAIProvider(AIProviderType.OpenAI)
                .SetApiKey("test-api-key")
                .SetModel("test-model")
                .SetDatabaseUri("test-database-uri")
                .Build();

            // Assert
            Assert.Equal(AIProviderType.OpenAI, configuration.AIProvider);
            Assert.Equal("test-api-key", configuration.ApiKey);
            Assert.Equal("test-model", configuration.Model);
            Assert.Equal("test-database-uri", configuration.DatabaseUri);
        }
    }
}
