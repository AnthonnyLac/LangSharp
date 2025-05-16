using LangSharp.Core.Configuration;
using LangSharp.Core.Services;

namespace LangSharp.UnitTests.Core.Services
{
    public class ConfigurationServiceTests
    {
        [Fact]
        public void SetEnvironmentConfigs_ShouldSetEnvironmentVariablesCorrectly()
        {
            // Arrange
            var configuration = new LangSharpConfiguration
            {
                ApiKey = "test-api-key",
                Model = "test-model",
                DatabaseUri = "test-database-uri"
            };

            var service = new ConfigurationService();

            // Act
            service.SetEnvironmentConfigs(configuration);

            // Assert
            Assert.Equal("test-api-key", Environment.GetEnvironmentVariable("LANGSHARP_API_KEY", EnvironmentVariableTarget.Process));
            Assert.Equal("test-model", Environment.GetEnvironmentVariable("LANGSHARP_MODEL", EnvironmentVariableTarget.Process));
            Assert.Equal("test-database-uri", Environment.GetEnvironmentVariable("LANGSHARP_DATABASE_URI", EnvironmentVariableTarget.Process));
        }
    }
}
