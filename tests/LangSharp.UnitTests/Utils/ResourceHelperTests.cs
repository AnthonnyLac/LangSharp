using LangSharp.Utils;

namespace LangSharp.UnitTests.Utils
{
    public class ResourceHelperTests
    {
        [Fact]
        public void ReadEmbeddedPythonScript_ShouldReturnContent_WhenScript_llm_sql_Exists()
        {
            // Arrange
            var scriptName = "llm.py"; 

            // Act
            var content = ResourceHelper.ReadEmbeddedPythonScript(scriptName);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Contains("call_llm_langsharp", content); 
        }

        [Fact]
        public void ReadEmbeddedPythonScript_ShouldReturnContent_WhenScript_llm_Exists()
        {
            // Arrange
            var scriptName = "llm_sql.py";

            // Act
            var content = ResourceHelper.ReadEmbeddedPythonScript(scriptName);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Contains("call_llm_sql_langsharp", content);
        }

        [Fact]
        public void ReadEmbeddedPythonScript_ShouldThrow_WhenScriptDoesNotExist()
        {
            // Arrange
            var scriptName = "notfound.py";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                ResourceHelper.ReadEmbeddedPythonScript(scriptName);
            });
        }
    }
}
