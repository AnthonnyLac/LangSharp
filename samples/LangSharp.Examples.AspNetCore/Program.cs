using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Services;
using LangSharp.MicrosoftExtensionsDI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Building the SDK configuration using the LangSharpConfigurationBuilder
var sdkConfiguration = new LangSharpConfigurationBuilder()
    .SetAIProvider(AIProviderType.LangChain)
    .SetModel("gpt-4o-mini")
    .SetApiKey("your-api-key")
    .SetDatabaseUri(default)
    .Build();

// Adding the services and the SDK configuration
builder.Services.AddLangSharp(sdkConfiguration);

var app = builder.Build();

//Debugging the AI chat service
var service = app.Services.GetService<ILangSharpService>();

var result = service!.CallAIChat(Console.ReadLine() ?? string.Empty);
Console.WriteLine(result);