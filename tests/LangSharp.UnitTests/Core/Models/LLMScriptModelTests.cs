using LangSharp.Core.Models;

namespace LangSharp.UnitTests.Core.Models
{
    public class LLMScriptModelTests
    {
        [Fact]
        public void ProcessMethod_ShouldInvokeMethodWithCorrectArguments()
        {
            // Arrange
            var name = "TestScript";
            var moduleName = "TestModule";
            var functionName = "TestFunction";
            var argsFunction = new object[] { "test-api-key", "test-prompt", "test-model" };

            var scriptModel = new LLMScriptModel(name, moduleName, functionName, argsFunction);

            // Define o método como um delegate explícito
            Func<string, string, string, string> mockMethod = (apiKey, prompt, model) =>
            {
                Assert.Equal("test-api-key", apiKey);
                Assert.Equal("test-prompt", prompt);
                Assert.Equal("test-model", model);
                return "Test Result";
            };

            // Act
            var result = scriptModel.ProcessMethod(mockMethod);

            // Assert
            Assert.Equal("Test Result", result);
        }

        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var name = "TestScript";
            var moduleName = "TestModule";
            var functionName = "TestFunction";
            var argsFunction = new object[] { "test-api-key", "test-prompt", "test-model" };

            // Act
            var scriptModel = new LLMScriptModel(name, moduleName, functionName, argsFunction);

            // Assert
            Assert.Equal(name, scriptModel.Name);
            Assert.Equal(moduleName, scriptModel.ModuleName);
            Assert.Equal(functionName, scriptModel.FunctionName);
            Assert.Equal(argsFunction, scriptModel.ArgsFunction);
        }
    }
}
