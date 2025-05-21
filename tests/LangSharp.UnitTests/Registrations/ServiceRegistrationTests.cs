using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;
using LangSharp.Registrations;
using Microsoft.Extensions.DependencyInjection;

namespace LangSharp.UnitTests.Registrations
{
    public class ServiceRegistrarTests
    {
        [Fact]
        public void AddRequiredServices_ShouldRegisterHandlers()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new LangSharpConfiguration
            {
                AIProvider = AIProviderType.LangChain,
                ApiKey = "test-api-key",
                Model = "test-model",
                DatabaseUri = "test-database-uri"
            };

            // Act
            ServiceRegistration.AddRequiredServices(services, configuration);

            // Assert
            Assert.Contains(services, s => s.ServiceType == typeof(IRequestValidatorHandler) && s.ImplementationType == typeof(RequestValidatorHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(IConfigurationSetupHandler) && s.ImplementationType == typeof(ConfigurationSetupHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(ISetEnvironmentVariablesHandler) && s.ImplementationType == typeof(SetEnvironmentVariablesHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(IVirtualEnvironmentHandler) && s.ImplementationType == typeof(VirtualEnvironmentHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(IPythonInstallationCheckerHandler) && s.ImplementationType == typeof(PythonInstallationCheckerHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(IPythonInitializerHandler) && s.ImplementationType == typeof(PythonInitializerHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(IPythonDependenciesInstallerHandler) && s.ImplementationType == typeof(PythonDependenciesInstallerHandler));
            Assert.Contains(services, s => s.ServiceType == typeof(ICommandExecutionHandler) && s.ImplementationType == typeof(CommandExecutionHandler));
        }

        [Fact]
        public void AddRequiredServices_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new LangSharpConfiguration
            {
                AIProvider = AIProviderType.LangChain,
                ApiKey = "test-api-key",
                Model = "test-model",
                DatabaseUri = "test-database-uri"
            };

            // Act
            ServiceRegistration.AddRequiredServices(services, configuration);

            // Assert
            Assert.Contains(services, s => s.ServiceType == typeof(ILangSharpService) && s.ImplementationType == typeof(LangSharpService));
            Assert.Contains(services, s => s.ServiceType == typeof(IPythonService) && s.ImplementationType == typeof(PythonService));
        }

        [Fact]
        public void AddRequiredServices_ShouldRegisterConfigurationAsSingleton()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new LangSharpConfiguration
            {
                AIProvider = AIProviderType.LangChain,
                ApiKey = "test-api-key",
                Model = "test-model",
                DatabaseUri = "test-database-uri"
            };

            // Act
            ServiceRegistration.AddRequiredServices(services, configuration);

            // Assert
            var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(LangSharpConfiguration));
            Assert.NotNull(serviceDescriptor);
            Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
            Assert.Equal(configuration, serviceDescriptor.ImplementationInstance);
        }

        [Fact]
        public void AddRequiredServices_ShouldRegisterAIProvider()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new LangSharpConfiguration
            {
                AIProvider = AIProviderType.LangChain,
                ApiKey = "test-api-key",
                Model = "test-model",
                DatabaseUri = "test-database-uri"
            };

            // Act
            ServiceRegistration.AddRequiredServices(services, configuration);

            // Build the service provider to resolve the AI provider
            var serviceProvider = services.BuildServiceProvider();
            var aiProvider = serviceProvider.GetService<ICloudAIProvider>();

            // Assert
            Assert.NotNull(aiProvider);
        }
    }
}
