using LangSharp.Core.Configuration;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Configurations;
using LangSharp.Core.Interfaces.Handlers.Base;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class ConfigurationSetupHandlerTests
    {
        [Fact]
        public void Handle_ShouldNotCallSetEnvironmentConfigs_WhenEnvironmentVariablesAreSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("OPENAI_API_KEY", "test-api-key", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_MODEL", "test-model", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", "test-database-uri", EnvironmentVariableTarget.Process);

            var configurationServiceMock = new Mock<IConfigurationService>();
            var configuration = new LangSharpConfiguration();
            var handler = new ConfigurationSetupHandler(configurationServiceMock.Object, configuration);

            // Act
            var result = handler.Handle(new object());

            // Assert
            configurationServiceMock.Verify(s => s.SetEnvironmentConfigs(It.IsAny<LangSharpConfiguration>()), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public void Handle_ShouldCallSetEnvironmentConfigs_WhenEnvironmentVariablesAreNotSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("OPENAI_API_KEY", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_MODEL", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", null, EnvironmentVariableTarget.Process);

            var configurationServiceMock = new Mock<IConfigurationService>();
            var configuration = new LangSharpConfiguration();
            var handler = new ConfigurationSetupHandler(configurationServiceMock.Object, configuration);

            // Act
            var result = handler.Handle(new object());

            // Assert
            configurationServiceMock.Verify(s => s.SetEnvironmentConfigs(configuration), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenEnvironmentVariablesAreSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("OPENAI_API_KEY", "test-api-key", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_MODEL", "test-model", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", "test-database-uri", EnvironmentVariableTarget.Process);

            var configurationServiceMock = new Mock<IConfigurationService>();
            var configuration = new LangSharpConfiguration();
            var nextHandlerMock = new Mock<IHandler>();

            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new ConfigurationSetupHandler(configurationServiceMock.Object, configuration);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
