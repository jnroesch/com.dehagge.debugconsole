using System.Linq;
using UnityEngine;

namespace Packages.com.dehagge.debugconsole.Runtime
{
    public class DebugConsoleController : MonoBehaviour
    {
        private bool showConsole;
        private bool showHelp;
        private Vector2 scroll;

        private string input;

        private DebugCommandCollection _commandCollection;

        //TODO [Inject]
        public void Initialize(DebugCommandCollection commandCollection)
        {
            _commandCollection = commandCollection;
        }

        private void Start()
        {
            //TODO remove, will be replaced by inject
            _commandCollection = new DebugCommandCollection();
            
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

            HandleInput();
            input = string.Empty;
        }

        private void HandleInput()
        {
            var splitStrings = input.Split(' ');
            var commandId = splitStrings.First();
            var parameters = splitStrings.Skip(1).ToArray();

            _commandCollection.GetCommand(commandId).Invoke(parameters);
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