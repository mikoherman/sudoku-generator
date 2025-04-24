namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Defines the contract for interacting with the user via the console.
/// </summary>
public interface IConsoleUserInteractor
{
    /// <summary>
    /// Reads a line of input from the console.
    /// </summary>
    /// <returns>
    /// The input entered by the user, or <c>null</c> if no input is provided.
    /// </returns>
    string? Read();
    /// <summary>
    /// Displays a message to the console.
    /// </summary>
    /// <param name="message">The message to display.</param>
    void ShowMessage(string message);
    /// <summary>
    /// Clears the console screen.
    /// </summary>
    void Clear();
}