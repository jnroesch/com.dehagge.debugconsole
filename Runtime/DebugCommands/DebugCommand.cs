using System;

namespace Packages.com.dehagge.debugconsole.Runtime.DebugCommands
{
    public class DebugCommand : DebugCommandBase
    {
        private readonly Action _command;
        
        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
        {
            _command = command;
        }

        public override void Invoke(string[] parameters)
        {
            if (parameters.Length != 0) return;
            
            _command.Invoke();
        }
    }

    public class DebugCommand<T> : DebugCommandBase
    {
        private readonly Action<T> _command;
        
        public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
        {
            _command = command;
        }

        public override void Invoke(string[] parameters)
        {
            if (parameters.Length != 1) return;
            var converted = TryParse<T>(parameters[0]);
            _command.Invoke(converted);
        }
    }
    
    public class DebugCommand<T1, T2> : DebugCommandBase
    {
        private readonly Action<T1, T2> _command;
        
        public DebugCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format)
        {
            _command = command;
        }

        public override void Invoke(string[] parameters)
        {
            if (parameters.Length != 2) return;
            var converted1 = TryParse<T1>(parameters[0]);
            var converted2 = TryParse<T2>(parameters[1]);
            _command.Invoke(converted1, converted2);
        }
    }
}