using System.Collections.Generic;
using Gloomwood.RuntimeConsole;
using ConsoleController = On.Gloomwood.RuntimeConsole.ConsoleController;

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
    
    public static void Initialize()
    {
        ConsoleController.Start += ConsoleControllerOnStart;
    }

    private static void ConsoleControllerOnStart(ConsoleController.orig_Start orig, Gloomwood.RuntimeConsole.ConsoleController self)
    {
        foreach (var command in GetCommands())
        {
            self.RegisterCommand(command);
        }

        orig(self);
    }
}