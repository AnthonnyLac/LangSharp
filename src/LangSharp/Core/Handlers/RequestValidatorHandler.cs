using LangSharp.Core.Abstractions;
using LangSharp.Core.Commands;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class RequestValidatorHandler : AbstractHandler, IRequestValidatorHandler
    {
        public RequestValidatorHandler()
        {
        }

        public override object Handle(object request)
        {
            if (request == null)
                return "Error: Request is null.";

            if (request is not CommandRequest commandRequest)
                return "Error: Request is not a CommandRequest.";

            if (string.IsNullOrEmpty(commandRequest.Parameter))
                return "Error: Request parameter is empty.";

            return base.Handle(request);
        }
    }
}
