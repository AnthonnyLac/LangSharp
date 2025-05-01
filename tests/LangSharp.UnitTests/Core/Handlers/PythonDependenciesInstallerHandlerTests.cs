using LangSharp.Core.Configuration;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Providers;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class PythonDependenciesInstallerHandlerTests
    {
        [Fact]
        public void Handle_ShouldReturnFailureMessage_WhenDependenciesInstallationFails()
        {
            // Arrange
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();
            cloudAIProviderMock
                .Setup(p => p.InstallDependencies())
                .Returns(false);

            var configuration = new LangSharpConfiguration();
            var handler = new PythonDependenciesInstallerHandler(cloudAIProviderMock.Object, configuration);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Failed to install dependencies", result);
            cloudAIProviderMock.Verify(p => p.InstallDependencies(), Times.Once);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenDependenciesInstallationSucceeds()
        {
            // Arrange
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();
            cloudAIProviderMock
                .Setup(p => p.InstallDependencies())
                .Returns(true);

            var configuration = new LangSharpConfiguration();
            var nextHandlerMock = new Mock<IHandler>();
            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new PythonDependenciesInstallerHandler(cloudAIProviderMock.Object, configuration);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            cloudAIProviderMock.Verify(p => p.InstallDependencies(), Times.Once);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
