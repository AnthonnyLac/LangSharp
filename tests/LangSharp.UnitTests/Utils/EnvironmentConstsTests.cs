using LangSharp.Utils;
using Moq;
using System.Reflection;

namespace LangSharp.UnitTests.Utils
{
    public class EnvironmentConstsTests
    {
        [Fact]
        public void PythonVersion_ShouldBeCorrect()
        {
            Assert.Equal("3.11.7", EnvironmentConsts.PythonVersion);
        }

        [Fact]
        public void GetAssemblyVersion_ShouldReturnCorrectVersion()
        {
            // Act
            var actualVersion = EnvironmentConsts.GetLangSharpAssemblyVersion();

            // Assert
            var versionParts = actualVersion.Split('.');
            Assert.Equal(3, versionParts.Length);
            Assert.All(versionParts, part => Assert.True(decimal.TryParse(part, out _), $"Version part '{part}' is not a valid number."));
        }


        [Fact]
        public void DllVersionName_ShouldBeCorrect()
        {
            Assert.Equal("python311.dll", EnvironmentConsts.DllVersionName);
        }

        [Fact]
        public void VirtualEnvironment_ShouldBeCorrect()
        {
            Assert.Equal("langsharp", EnvironmentConsts.VirtualEnvironment);
        }
    }
}
