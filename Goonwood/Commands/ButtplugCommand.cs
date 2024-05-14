using Gloomwood.RuntimeConsole;
using Gloomwood.RuntimeConsole.Commands;

namespace Goonwood.Commands;

public class ButtplugCommand
{
    public static ConsoleCommand GetCommand()
    {
        return new ConsoleCommand(Name, Description, Usage, Execute);
    }

    public static string Execute(params string[] args)
    {
        var command = args[0];

        return command switch
        {
            "reconnect" => Reconnect(),
            "disconnect" => Disconnect(),
            "status" => $"Connected: {Goonwood.DeviceManager.IsConnected()}",
            _ => HelpCommand.Execute(new[] { Usage }),
        };
    }

    private static string Disconnect()
    {
        Goonwood.DeviceManager.Disconnect();
        
        return "Disconnected";
    }
    
    private static string Reconnect()
    {
        Goonwood.DeviceManager.Reconnect();

        return "Reconnecting... hopefully...";
    }

    public static string Name = "bp";

    public static string Description = "Goonwood Buttplug commands";

    public static string Usage = "bp [reconnect] | [disconnect] | [status]";
}