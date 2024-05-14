using System.Collections.Generic;
using Gloomwood.RuntimeConsole;

namespace Goonwood.Commands;

public class CommandLoader
{
    private static List<ConsoleCommand> GetCommands()
    {
        return new List<ConsoleCommand>()
        {
            ButtplugCommand.GetCommand(),
        };
    }
    
    public static void LoadCommands()
    {
        foreach (var command in GetCommands())
        {
            ConsoleController.Instance.RegisterCommand(command);
        }
    }
}