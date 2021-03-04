using System.Collections.Generic;
using System.Linq;

namespace Packages.com.dehagge.debugconsole.Runtime
{
    public class DebugCommandCollection
    {
        private readonly Dictionary<string, DebugCommandBase> _collection;

        public DebugCommandCollection()
        {
            _collection = new Dictionary<string, DebugCommandBase>();
        }

        public DebugCommandBase GetCommand(string command)
        {
            return _collection[command];
        }
        
        public void AddDebugCommand(DebugCommandBase command)
        {
            _collection.Add(command.CommandId, command);
        }

        public List<DebugCommandBase> GetAvailableCommands()
        {
            return _collection.Values.ToList();
        }
    }
}