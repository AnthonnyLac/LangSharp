using LangSharp.Core.Services;

namespace LangSharp.UnitTests.Core.Services
{

    public class FileSystemServiceTests
    {
        [Fact]
        public void IsValidDirectory_ShouldReturnTrue_ForExistingDirectory()
        {
            // Arrange
            var service = new FileSystemService();
            var tempDir = Path.GetTempPath();

            // Act
            var result = service.IsValidDirectory(tempDir);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidDirectory_ShouldReturnFalse_ForNullOrEmptyOrNonExistingDirectory()
        {
            var service = new FileSystemService();

            Assert.False(service.IsValidDirectory(null));
            Assert.False(service.IsValidDirectory(""));
            Assert.False(service.IsValidDirectory("C:\\this\\directory\\should\\not\\exist\\123456"));
        }

        [Fact]
        public void IsFileExist_ShouldReturnTrue_ForExistingFile()
        {
            // Arrange
            var service = new FileSystemService();
            var tempFile = Path.GetTempFileName();

            // Act
            var result = service.IsFileExist(tempFile);

            // Assert
            Assert.True(result);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void IsFileExist_ShouldReturnFalse_ForNullOrEmptyOrNonExistingFile()
        {
            var service = new FileSystemService();

            Assert.False(service.IsFileExist(null));
            Assert.False(service.IsFileExist(""));
            Assert.False(service.IsFileExist("C:\\this\\file\\should\\not\\exist\\123456.txt"));
        }

        [Fact]
        public void WriteEmbeddedPythonScriptToProjectRoot_ShouldWriteFileAndReturnPath_WhenScriptExists()
        {
            // Arrange
            var service = new FileSystemService();
            var scriptName = "llm.py"; // Certifique-se que este script está embutido como recurso

            // Act
            var scriptPath = service.WriteEmbeddedPythonScriptToProjectRoot(scriptName);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(scriptPath));
            Assert.True(File.Exists(scriptPath));

            // Cleanup
            if (scriptPath != null && File.Exists(scriptPath))
                File.Delete(scriptPath);
        }

        [Fact]
        public void WriteEmbeddedPythonScriptToProjectRoot_ShouldReturnNull_WhenScriptDoesNotExist()
        {
            // Arrange
            var service = new FileSystemService();
            var scriptName = "notfound.py";

            // Act
            var scriptPath = service.WriteEmbeddedPythonScriptToProjectRoot(scriptName);

            // Assert
            Assert.Null(scriptPath);
        }
    }
}
