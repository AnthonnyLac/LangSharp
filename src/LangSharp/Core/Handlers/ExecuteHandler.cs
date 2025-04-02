using LangSharp.Core.Abstractions;
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
            var command = request.ToString()!;

            var result = _cloudAIProvider.GetResponse(command);

            if (!string.IsNullOrEmpty(result))
                return result;

            return base.Handle(result);
        }
    }
}
