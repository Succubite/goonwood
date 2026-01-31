using System.Collections.Generic;
using Gloomwood.RuntimeConsole;
using MonoDetour;
using MonoDetour.HookGen;

namespace Goonwood.Commands;

[MonoDetourTargets(typeof(ConsoleController))]
public static class CommandInitializer
{
    private static readonly List<ICommand> Commands = [];

    [MonoDetourHookInitialize]
    public static void Initialize()
    {
        Md.Gloomwood.RuntimeConsole.ConsoleController.Start.Prefix(Prefix_Start);
    }

    public static void AddCommand(ICommand command)
    {
        Goonwood.Log.LogDebug($"Adding console command: {command.Name}");
        Commands.Add(command);
    }

    private static void Prefix_Start(ConsoleController self)
    {
        foreach (var command in Commands)
        {
            self.RegisterCommand(command.GetCommand());
        }
    }
}