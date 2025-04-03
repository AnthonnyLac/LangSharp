using LangSharp.Core.Enums;

namespace LangSharp.Core.Commands
{
    public class CommandRequest
    {
        public TypeCommand CommandType { get; set; }
        public string Parameter { get; set; }

        public CommandRequest(TypeCommand commandType, string parameter)
        {
            CommandType = commandType;
            Parameter = parameter;
        }
    }
}
