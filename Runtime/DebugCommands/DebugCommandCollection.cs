using System;
using System.Collections.Generic;
using System.Linq;

namespace Packages.com.dehagge.debugconsole.Runtime.DebugCommands
{
    public class DebugCommandCollection
    {
        private readonly Dictionary<string, DebugCommandBase> _collection;

        public DebugCommandCollection()
        {
            _collection = new Dictionary<string, DebugCommandBase>();
        }

        public DebugCommandBase GetCommand(string commandId)
        {
            DebugCommandBase command;
            try
            {
                command = _collection[commandId];
            }
            catch (Exception)
            {
                command = null;
            }

            return command;
        }
        
        public void AddDebugCommand(DebugCommandBase command)
        {
            _collection.Add(command.CommandId, command);
        }

        public IEnumerable<DebugCommandBase> GetAvailableCommands()
        {
            return _collection.Values.ToList();
        }
    }
}