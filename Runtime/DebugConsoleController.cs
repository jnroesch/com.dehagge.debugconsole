using System.Linq;
using Packages.com.dehagge.debugconsole.Runtime.DebugCommandHandler;
using Packages.com.dehagge.debugconsole.Runtime.DebugCommands;
using UnityEngine;
using Zenject;

namespace Packages.com.dehagge.debugconsole.Runtime
{
    public class DebugConsoleController : MonoBehaviour
    {
        private bool showConsole;
        private bool showHelp;
        private Vector2 scroll;

        private string input;

        private DebugCommandCollection _commandCollection;
        private IDebugCommandHandler _debugCommandHandler;

        [Inject]
        public void Initialize(IDebugCommandHandler debugCommandHandler, DebugCommandCollection commandCollection)
        {
            _debugCommandHandler = debugCommandHandler;
            _commandCollection = commandCollection;
        }

        private void Start()
        {
            _commandCollection.AddDebugCommand(new DebugCommand("help", "displays all available commands", "help", () =>
            {
                showHelp = true;
            }));
        }

        //TODO: replace with new input system and DI
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ToggleDebugConsole();
            }
            
            if (!Input.GetKeyDown(KeyCode.Return) || string.IsNullOrWhiteSpace(input)) return;

            _debugCommandHandler.HandleConsoleInput(input);
            input = string.Empty;
        }


        private void ToggleDebugConsole()
        {
            showHelp = false;
            showConsole = !showConsole;
        }

        private void OnGUI()
        {
            if (!showConsole) return;

            var y = 0f;

            if (showHelp)
            {
                var commandList = _commandCollection.GetAvailableCommands().OrderBy(command => command.CommandId).ToList();
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                var viewPort = new Rect(0,0, Screen.width-30, 20 * commandList.Count);
                scroll = GUI.BeginScrollView(new Rect(0, y+5f, Screen.width, 90), scroll, viewPort);
                for (var i = 0; i < commandList.Count; i++)
                {
                    var command = commandList[i];
                    var label = $"{command.CommandFormat} - {command.CommandDescription}";
                    var labelRect = new Rect(5, 20 * i, Screen.width - 100, 20);
                    GUI.Label(labelRect,label);
                }
                GUI.EndScrollView();
                y += 100;
            }

            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20, 20f), input);
        }
    }
}