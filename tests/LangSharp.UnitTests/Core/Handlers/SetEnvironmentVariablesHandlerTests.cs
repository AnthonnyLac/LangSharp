using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class SetEnvironmentVariablesHandlerTests
    {
        [Fact]
        public void Handle_ShouldNotCallConfigureEnvironmentPaths_WhenEnvironmentVariablesAreSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("PYTHONHOME", "test-python-home", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", "test-python-path", EnvironmentVariableTarget.Process);

            var pythonServiceMock = new Mock<IPythonService>();
            var handler = new SetEnvironmentVariablesHandler(pythonServiceMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            pythonServiceMock.Verify(p => p.ConfigureEnvironmentPaths(), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public void Handle_ShouldCallConfigureEnvironmentPaths_WhenEnvironmentVariablesAreNotSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("PYTHONHOME", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", null, EnvironmentVariableTarget.Process);

            var pythonServiceMock = new Mock<IPythonService>();
            var handler = new SetEnvironmentVariablesHandler(pythonServiceMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            pythonServiceMock.Verify(p => p.ConfigureEnvironmentPaths(), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenEnvironmentVariablesAreSet()
        {
            // Arrange
            Environment.SetEnvironmentVariable("PYTHONHOME", "test-python-home", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", "test-python-path", EnvironmentVariableTarget.Process);

            var pythonServiceMock = new Mock<IPythonService>();
            var nextHandlerMock = new Mock<IHandler>();

            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new SetEnvironmentVariablesHandler(pythonServiceMock.Object);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            pythonServiceMock.Verify(p => p.ConfigureEnvironmentPaths(), Times.Never);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
