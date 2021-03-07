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
        
        public void HandleConsoleInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;
            
            var splitStrings = input.Split(' ');
            var commandId = splitStrings.First();
            var parameters = splitStrings.Skip(1).ToArray();

            _commandCollection.GetCommand(commandId).Invoke(parameters);
        }
    }
}