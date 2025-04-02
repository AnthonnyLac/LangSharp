using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class ExecuteHandler : AbstractHandler, IExecuteHandler
    {
        private readonly IPythonService _pythonService;

        public ExecuteHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            var command = request.ToString()!;
            var result = _pythonService.ExecuteCommand(command);

            if (!string.IsNullOrEmpty(result))
                return result;

            return base.Handle(result);
        }
    }
}
