using System.ComponentModel;
using System.Globalization;

namespace Packages.com.dehagge.debugconsole.Runtime.DebugCommands
{
    public abstract class DebugCommandBase
    {
        public string CommandId { get; }
        public string CommandDescription { get; }
        public string CommandFormat { get; }

        public DebugCommandBase(string id, string description, string format)
        {
            CommandId = id;
            CommandDescription = description;
            CommandFormat = format;
        }

        public abstract void Invoke(string[] parameters);

        protected static T TryParse<T>(string val)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var converted = (T) converter.ConvertFromString(null, CultureInfo.InvariantCulture, val);
            return converted;
        }
    }
}