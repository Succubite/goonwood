using System.Collections.Generic;
using Gloomwood.RuntimeConsole;

namespace Goonwood.Commands;

public class CommandLoader
{
    public static List<ConsoleCommand> LoadCommands()
    {
        return new List<ConsoleCommand>()
        {
            ButtplugCommand.GetCommand(),
        };
    }
}