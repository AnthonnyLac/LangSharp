using LangSharp.Core.Enums;

namespace LangSharp.Core.Commands
{
    public class CommandRequest
    {
        public TypeCommand CommandType { get; private set; }
        public string Parameter { get; private set; }

        public CommandRequest(TypeCommand commandType, string parameter)
        {
            CommandType = commandType;
            Parameter = parameter;
        }
    }
}
