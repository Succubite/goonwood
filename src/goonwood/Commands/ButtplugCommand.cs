using Gloomwood.RuntimeConsole;
using Gloomwood.RuntimeConsole.Commands;

namespace Goonwood.Commands;

public static class ButtplugCommand
{
    public static ConsoleCommand GetCommand()
    {
        return new ConsoleCommand(Name, Description, Usage, Execute);
    }

    public static string Execute(params string[] args)
    {
        if (args.Length == 0) return HelpCommand.Execute(Usage);

        var command = args[0];

        return command switch
        {
            "disconnect" => Disconnect(),
            "devices" => ShowDevices(),
            "status" => $"Connected: {Goonwood.DeviceManager.IsConnected()}",
            _ => HelpCommand.Execute(Usage),
        };
    }

    private static string ShowDevices()
    {
        var devices = "";

        foreach (var device in Goonwood.DeviceManager.ConnectedDevices)
        {
            devices += $"Connected device: {device.Name}\n";
        }

        return devices;
    }

    private static string Disconnect()
    {
        Goonwood.DeviceManager.Disconnect();

        return "Disconnected";
    }

    public static string Name = "bp";

    public static string Description = "Goonwood Buttplug commands";

    public static string Usage = "bp [disconnect] | [status | [devices]";
}