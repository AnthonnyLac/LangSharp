using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;

namespace LangSharp.UnitTests.Core.Configuration
{
    public class LangSharpConfigurationTests
    {
        [Fact]
        public void Properties_ShouldAllowGetAndSet()
        {
            // Arrange
            var configuration = new LangSharpConfiguration();

            // Act
            configuration.AIProvider = AIProviderType.LangChain;
            configuration.ApiKey = "test-api-key";
            configuration.Model = "test-model";
            configuration.DatabaseUri = "test-database-uri";

            // Assert
            Assert.Equal(AIProviderType.LangChain, configuration.AIProvider);
            Assert.Equal("test-api-key", configuration.ApiKey);
            Assert.Equal("test-model", configuration.Model);
            Assert.Equal("test-database-uri", configuration.DatabaseUri);
        }
    }
}
