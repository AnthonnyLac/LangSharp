# LangSharp SDK

LangSharp SDK is a .NET 8 library that leverages Python.NET to communicate with AI providers, utilizing powerful Python resources such as LangChain. This SDK provides a robust framework for executing Python commands and scripts within a .NET environment, enabling seamless integration with various AI services.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
  - [Executing Queries](#executing-queries)
  - [Calling AI Cloud](#calling-ai-cloud)
- [License](#license)

## Introduction

LangSharp SDK is designed to bridge the gap between .NET applications and Python's extensive ecosystem of AI tools. By using Python.NET, this SDK allows .NET developers to harness the power of Python libraries such as LangChain, enabling advanced AI functionalities within their .NET applications.

## Features
- **AI Provider Support**: Communicate with AI providers like OpenAI using Python scripts.
- **Chain Requests**: Create chains through Python to make requests to AI providers.

## Getting Started

### Installation

1. **Install the LangSharp SDK**:

```shell
   dotnet add package LangSharp.SDK
```

### Configuration

Configure the LangSharp SDK using the builder pattern in your `Program.cs`:


```csharp
using LangSharp.Core.Configuration;
using LangSharp.Core.Enums;
using LangSharp.MicrosoftExtensionsDI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Building the SDK configuration using the LangSharpConfigurationBuilder
var sdkConfiguration = new LangSharpConfigurationBuilder()
    .SetAIProvider(AIProviderType.OpenAI)
    .SetModel("gpt-4o-mini")
    .SetApiKey("your-openai-api-key")
    .SetDatabaseUri("your-database-uri") // Optional
    .Build();

// Adding the services and the SDK configuration
builder.Services.AddLangSharp(sdkConfiguration);

var app = builder.Build();

app.Run();
```

## Usage

### Executing Queries

To execute a database query using the LangSharp SDK, you can ask a question about the table:

```csharp
using LangSharp.Core.Interfaces.Services;

var service = app.Services.GetService<ILangSharpService>();
string queryResult = await service.ExecuteDatabaseQueryAsync("What are the names of all users in the users table?");
Console.WriteLine(queryResult);

```

### Calling AI Cloud

To call an AI cloud service using the LangSharp SDK:

```csharp
using LangSharp.Core.Interfaces.Services;

var service = app.Services.GetService<ILangSharpService>();
string aiResponse = await service.CallAIChatAsync("What is the weather like today?");
Console.WriteLine(aiResponse);
```

## Running in Docker Containers

To run the LangSharp SDK in a Docker container, ensure Python and required dependencies are properly configured. Below are examples for Linux and Windows containers.

### Linux

Install Python 3.11.x:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app

# Install Python 3.11 and dependencies
RUN apt update && \
    apt install -y \
    python3.11 \
    python3-pip \
    python3-venv \
    && rm -rf /var/lib/apt/lists/*
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
