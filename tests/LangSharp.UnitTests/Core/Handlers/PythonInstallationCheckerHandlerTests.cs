using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class PythonInstallationCheckerHandlerTests
    {
        [Fact]
        public void Handle_ShouldReturnErrorMessage_WhenPythonIsNotInstalled()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.IsPythonEnvironmentInstalled())
                .Returns(false);

            var handler = new PythonInstallationCheckerHandler(pythonServiceMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Error: Python is not installed.", result);
            pythonServiceMock.Verify(p => p.IsPythonEnvironmentInstalled(), Times.Once);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenPythonIsInstalled()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.IsPythonEnvironmentInstalled())
                .Returns(true);

            var nextHandlerMock = new Mock<IHandler>();
            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new PythonInstallationCheckerHandler(pythonServiceMock.Object);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            pythonServiceMock.Verify(p => p.IsPythonEnvironmentInstalled(), Times.Once);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
