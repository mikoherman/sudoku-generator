namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Provides methods for interacting with the user via the console.
/// </summary>
public class ConsoleUserInteractor : IConsoleUserInteractor
{
    /// <summary>
    /// Reads a line of input from the console.
    /// </summary>
    /// <returns>
    /// The input entered by the user, or <c>null</c> if no input is provided.
    /// </returns>
    public string? Read() =>
        Console.ReadLine();
    /// <summary>
    /// Displays a message to the console.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void ShowMessage(string message) =>
        Console.WriteLine(message);
    /// <summary>
    /// Clears the console screen.
    /// </summary>
    public void Clear() =>
        Console.Clear();
}
