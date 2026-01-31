using System;
using System.Linq;
using Gloomwood.AI;
using Gloomwood.Entity.AI;
using Gloomwood.RuntimeConsole.Commands;

namespace Goonwood.Commands;

public class AlertLevelCommand : ICommand
{
    public string Name => "alertlevel";
    public string Description => "Forcefully set alert level of all entities";
    public string Usage => "alertlevel [level]";
    
    public string Execute(params string[] args)
    {
        if (args.Length == 0) return HelpCommand.Execute(Name);
        
        var level = args[0];
        if (!Enum.TryParse<AIAlertLevel>(level, out var alertLevel)) return HelpCommand.Execute(Name);
        
        SetAlertLevel(alertLevel);
        
        return $"Set alert level to {alertLevel}";
    }

    private static void SetAlertLevel(AIAlertLevel level)
    {
        var activeAI = AIManager.activeAI;
        var sensingEntities = activeAI.Where(entity => 
            entity.HasComponent(AIComponentFlags.Sense) && 
            (!entity.HasComponent(AIComponentFlags.Health) || entity.Health.IsAlive));

        foreach (var entity in sensingEntities)
        {
            entity.Sense.alertLevel = level;
        }
    }
}