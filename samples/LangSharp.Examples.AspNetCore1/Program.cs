using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Registrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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


//Debugging the AI chat service
var service = app.Services.GetService<ILangSharpService>();

await service!.CallAIChatAsync("print('Hello, how are you?')");
