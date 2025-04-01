using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.Registrations;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Building the SDK configuration using the LangSharpConfigurationBuilder
var sdkConfiguration = new LangSharpConfigurationBuilder()
    .SetAIProvider(AIProviderType.OpenAI)
    .SetApiKey("your-api-key")
    .SetPythonEnvironment("path/to/python/environment")
    .Build();

// Adding the services and the SDK configuration
ServiceRegistrar.AddRequiredServices(builder.Services, sdkConfiguration);

var app = builder.Build();

app.Run();