using System.Linq;
using Packages.com.dehagge.debugconsole.Runtime.DebugCommands;
using Zenject;

namespace Packages.com.dehagge.debugconsole.Runtime.DebugCommandHandler
{
    public class DebugCommandHandler : IDebugCommandHandler
    {
        private readonly DebugCommandCollection _commandCollection;
        
        [Inject]
        public DebugCommandHandler(DebugCommandCollection collection)
        {
            _commandCollection = collection;
        }
        
        public bool HandleConsoleInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            
            var splitStrings = input.Split(' ');
            var commandId = splitStrings.First();
            var parameters = splitStrings.Skip(1).ToArray();

            var command = _commandCollection.GetCommand(commandId);

            if(command == null) return false;
            command.Invoke(parameters);
            return true;
        }
    }
}