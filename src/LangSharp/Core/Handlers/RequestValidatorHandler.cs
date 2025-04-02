using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class RequestValidatorHandler : AbstractHandler, IValidatorHandler
    {
        public RequestValidatorHandler()
        {
        }

        public override object Handle(object request)
        {
            if (request == null)
                return "Error: Request is null.";

            if (request is not string)
                return "Error: Request is not a string.";

            if (string.IsNullOrEmpty(request as string))
                return "Error: Request is empty.";

            return base.Handle(request);
        }
    }
}
