using LangSharp.Core.Abstractions;
using LangSharp.Core.Commands;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class ExecuteHandler : AbstractHandler, IExecuteHandler
    {
        private readonly ICloudAIProvider _cloudAIProvider;

        public ExecuteHandler(IPythonService pythonService, ICloudAIProvider cloudAIProvider)
        {
            _cloudAIProvider = cloudAIProvider;
        }

        public override object Handle(object request)
        {
            if (request is not CommandRequest commandRequest)
            {
                return base.Handle(request);
            }

            string result = commandRequest.CommandType switch
            {
                TypeCommand.ExecuteDatabaseQuery => _cloudAIProvider.ExecuteDatabaseQuery(commandRequest.Parameter),
                TypeCommand.GetResponse => _cloudAIProvider.GetResponse(commandRequest.Parameter),
                _ => "Unknown command"
            };

            if (!string.IsNullOrEmpty(result))
                return result;

            return base.Handle(result);

        }
    }
}
