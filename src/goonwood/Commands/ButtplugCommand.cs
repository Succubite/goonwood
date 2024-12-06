using Gloomwood.RuntimeConsole.Commands;
using GloomwoodTesting.Commands;

namespace Goonwood.Commands;

public class ButtplugCommand : ICommand
{
    public string Name => "buttplug";
    public string Description => "Goonwood buttplug commands, for development purposes";
    public string Usage => "buttplug [devices | disconnect | connect]";

    public string Execute(params string[] args)
    {
        if (args.Length == 0) return HelpCommand.Execute(Name);

        var argument = args[0];

        return argument switch
        {
            "connect" => Connect(args[1]),
            "disconnect" => Disconnect(),
            "devices" => ShowDevices(),
            _ => HelpCommand.Execute(Usage)
        };
    }

    private static string Connect(string url)
    {
        Goonwood.DeviceManager.Reconnect(url);
        
        return $"Attempting to connect to: {url}";
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
}