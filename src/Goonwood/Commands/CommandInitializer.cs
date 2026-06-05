using System.Collections.Generic;
using Gloomwood;
using MonoDetour;
using MonoDetour.HookGen;

namespace Goonwood.Commands;

[MonoDetourTargets(typeof(Console))]
public static class CommandInitializer
{
    private static readonly List<ICommand> Commands = [];

    [MonoDetourHookInitialize]
    public static void Initialize()
    {
        Md.Gloomwood.Console.RegisterCommands.Prefix(Prefix_Start);
    }

    public static void AddCommand(ICommand command)
    {
        Goonwood.Log.LogDebug($"Adding console command: {command.Name}");
        Commands.Add(command);
    }

    private static void Prefix_Start()
    {
        foreach (var command in Commands)
        {
            Console.RegisterCommand(command.GetCommand());
        }
    }
}
