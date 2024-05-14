using Gloomwood.RuntimeConsole;

namespace Goonwood.Commands;

public class ReconnectCommand
{
    public static ConsoleCommand GetCommand()
    {
        return new ConsoleCommand(Name, Description, Usage, Execute);
    }

    public static string Execute(params string[] args)
    {
        // TODO: this...
        return "";
    }

    public static string Name = "BP-RECONNECT";

    public static string Description = "Reconnect to the Buttplug server";

    public static string Usage = "BP-RECONNECT";
}