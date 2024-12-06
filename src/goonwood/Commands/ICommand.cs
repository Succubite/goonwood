using Gloomwood.RuntimeConsole;

namespace GloomwoodTesting.Commands;

/// <summary>
/// Interface representing the general functionality of a console command.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// The name of the console command, (e.g., "give").
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// The description of the console command (e.g., "Gives the player an item").
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// A short explanation on how to use the console command (e.g., "give [item_name]").
    /// </summary>
    public string Usage { get; }

    /// <summary>
    /// Creates a new console command structure to add to the available console commands.
    /// </summary>
    public ConsoleCommand GetCommand()
    {
        return new ConsoleCommand(Name, Description, Usage, Execute);
    }

    /// <summary>
    /// A function to be executed when the console command is ran.
    /// </summary>
    public string Execute(params string[] args);
}