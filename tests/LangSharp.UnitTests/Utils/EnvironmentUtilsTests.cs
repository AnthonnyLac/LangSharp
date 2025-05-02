using LangSharp.Utils;

namespace LangSharp.UnitTests.Utils
{
    public class EnvironmentUtilsTests
    {
        [Fact]
        public void GetNugetPackageDirPath_ShouldReturnCorrectPath()
        {
            var expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
            Assert.Equal(expectedPath, EnvironmentUtils.GetNugetPackageDirPath());
        }

        [Fact]
        public void GetNugetPythonRoot_ShouldReturnCorrectPath()
        {
            var expectedPath = Path.Combine(EnvironmentUtils.GetNugetPackageDirPath(), "python", EnvironmentConsts.PythonVersion);
            Assert.Equal(expectedPath, EnvironmentUtils.GetNugetPythonRoot());
        }

        [Fact]
        public void GetPythonPath_ShouldReturnCorrectPath()
        {
            var expectedPath = Path.Combine(EnvironmentUtils.GetNugetPythonRoot(), "tools");
            Assert.Equal(expectedPath, EnvironmentUtils.GetPythonPath());
        }

        [Fact]
        public void GetVenvPath_ShouldReturnCorrectPath()
        {
            var expectedPath = Path.Combine(EnvironmentUtils.GetNugetPythonRoot(), EnvironmentConsts.VirtualEnvironment);
            Assert.Equal(expectedPath, EnvironmentUtils.GetVenvPath());
        }

        [Fact]
        public void GetSitePackagesPath_ShouldReturnCorrectPath()
        {
            var pythonHome = "C:\\Python";
            var expectedPath = Path.Combine(pythonHome, "Lib", "site-packages");
            Assert.Equal(expectedPath, EnvironmentUtils.GetSitePackagesPath(pythonHome));
        }

        [Fact]
        public void GetScriptsPath_ShouldReturnCorrectPath()
        {
            var scriptName = "test.py";
            var expectedPath = Path.Combine(AppContext.BaseDirectory, "scripts", scriptName);
            Assert.Equal(expectedPath, EnvironmentUtils.GetScriptsPath(scriptName));
        }

        [Fact]
        public void GetPythonDllPath_ShouldReturnCorrectPath()
        {
            var expectedPath = Path.Combine(EnvironmentUtils.GetPythonPath(), EnvironmentConsts.DllVersionName);
            Assert.Equal(expectedPath, EnvironmentUtils.GetPythonDllPath());
        }

        [Fact]
        public void GetPythonHomeFromEnvironment_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", "C:\\Python", EnvironmentVariableTarget.Process);
            Assert.Equal("C:\\Python", EnvironmentUtils.GetPythonHomeFromEnvironment());
        }

        [Fact]
        public void GetPythonPathFromEnvironment_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("PYTHONPATH", "C:\\Python\\Lib", EnvironmentVariableTarget.Process);
            Assert.Equal("C:\\Python\\Lib", EnvironmentUtils.GetPythonPathFromEnvironment());
        }

        [Fact]
        public void GetPythonPathExecutable_ShouldReturnCorrectPath_ForVirtualEnv()
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", "C:\\Python\\langsharp", EnvironmentVariableTarget.Process);
            var expectedPath = Path.Combine("C:\\Python\\langsharp", "Scripts", "python.exe");
            Assert.Equal(expectedPath, EnvironmentUtils.GetPythonPathExecutable());
        }

        [Fact]
        public void GetPythonPathExecutable_ShouldReturnCorrectPath_ForNonVirtualEnv()
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", "C:\\Python", EnvironmentVariableTarget.Process);
            var expectedPath = Path.Combine("C:\\Python", "python.exe");
            Assert.Equal(expectedPath, EnvironmentUtils.GetPythonPathExecutable());
        }

        [Fact]
        public void GetPythonPathExecutable_ShouldReturnNull_WhenPythonHomeIsNotSet()
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", null, EnvironmentVariableTarget.Process);
            Assert.Null(EnvironmentUtils.GetPythonPathExecutable());
        }

        [Fact]
        public void GetNugetLangSharpRoot_ShouldReturnCorrectPath()
        {
            // Arrange
            var expectedPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget",
                "packages",
                "langsharp",
                EnvironmentConsts.GetLangSharpAssemblyVersion()
            );

            // Act
            var actualPath = EnvironmentUtils.GetNugetLangSharpRoot();

            // Assert
            Assert.Equal(expectedPath, actualPath);
        }

        [Fact]
        public void GetScriptsPathByPackageDir_ShouldReturnCorrectPath()
        {
            // Arrange
            var scriptName = "test_script.py";
            var expectedPath = Path.Combine(
                EnvironmentUtils.GetNugetLangSharpRoot(),
                "Scripts",
                scriptName
            );

            // Act
            var actualPath = EnvironmentUtils.GetScriptsPathByPackageDir(scriptName);

            // Assert
            Assert.Equal(expectedPath, actualPath);
        }
    }
}
