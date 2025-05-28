using LangSharp.Core.Services;

namespace LangSharp.UnitTests.Core.Services
{
    public class EnvironmentServiceTests
    {
        [Fact]
        public void ConfigurePythonEnvironment_WithDll_ShouldSetEnvironmentVariablesCorrectly()
        {
            // Arrange
            var service = new EnvironmentService();
            var pythonHome = "C:\\Python";
            var sitePackagesPath = "C:\\Python\\Lib\\site-packages";
            var pythonDllPath = "C:\\Python\\python311.dll";

            // Act
            service.ConfigurePythonEnvironment(pythonHome, sitePackagesPath, pythonDllPath);

            // Assert
            Assert.Equal(pythonDllPath, Environment.GetEnvironmentVariable("PYTHONNET_PYDLL", EnvironmentVariableTarget.Process));
            Assert.Equal(pythonHome, Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process));
            Assert.Equal(sitePackagesPath, Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process));
            Assert.Equal("False", Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process));
        }

        [Fact]
        public void ConfigurePythonEnvironment_WithoutDll_ShouldSetEnvironmentVariablesCorrectly()
        {
            // Arrange
            var service = new EnvironmentService();
            var pythonHome = "/usr/bin/python";
            var sitePackagesPath = "/usr/lib/python3.11/site-packages";

            // Act
            service.ConfigurePythonEnvironment(pythonHome, sitePackagesPath);

            // Assert
            Assert.Equal(pythonHome, Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process));
            Assert.Equal(sitePackagesPath, Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process));
        }

        [Fact]
        public void ConfigurePythonVirtualEnvironment_ShouldSetPythonPathAndIsVenv()
        {
            // Arrange
            var service = new EnvironmentService();
            var sitePackagesPath = "/venv/lib/python3.11/site-packages";
            var isVenv = true;

            // Limpa variáveis para garantir ambiente limpo
            Environment.SetEnvironmentVariable("PYTHONPATH", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", null, EnvironmentVariableTarget.Process);

            // Act
            service.ConfigurePythonVirtualEnvironment(sitePackagesPath, isVenv);

            // Assert
            Assert.Equal(sitePackagesPath, Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process));
            Assert.Equal("True", Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process));
        }

        [Fact]
        public void ConfigurePythonVirtualEnvironment_ShouldAppendToExistingPythonPath()
        {
            // Arrange
            var service = new EnvironmentService();
            var existingPath = "/usr/lib/python3.11/site-packages";
            var sitePackagesPath = "/venv/lib/python3.11/site-packages";
            var isVenv = false;

            // Prepara variável existente
            Environment.SetEnvironmentVariable("PYTHONPATH", existingPath, EnvironmentVariableTarget.Process);

            // Act
            service.ConfigurePythonVirtualEnvironment(sitePackagesPath, isVenv);

            // Assert
            var expected = $"{sitePackagesPath};{existingPath}";
            Assert.Equal(expected, Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process));
            Assert.Equal("False", Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process));
        }
    }
}
