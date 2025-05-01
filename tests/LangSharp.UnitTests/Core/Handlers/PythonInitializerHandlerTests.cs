using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class PythonInitializerHandlerTests
    {
        [Fact]
        public void Handle_ShouldCallInitializePythonEngine()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var handler = new PythonInitializerHandler(pythonServiceMock.Object);

            // Act
            handler.Handle(new object());

            // Assert
            pythonServiceMock.Verify(p => p.InitializePythonEngine(), Times.Once);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var nextHandlerMock = new Mock<IHandler>();

            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new PythonInitializerHandler(pythonServiceMock.Object);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            pythonServiceMock.Verify(p => p.InitializePythonEngine(), Times.Once);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
