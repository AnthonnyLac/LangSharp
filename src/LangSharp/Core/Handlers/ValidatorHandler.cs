using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class ValidatorHandler : AbstractHandler, IValidatorHandler
    {
        public ValidatorHandler()
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
