using Sudoku_Generator.Events;

namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Defines the contract for displaying looping status messages until a process is completed.
/// </summary>
public interface ILoopingStatusPrinter
{
    /// <summary>
    /// Displays a looping status message until the process is completed.
    /// </summary>
    /// <param name="messageBase">The base message to display.</param>
    /// <param name="messageUponCompletion">The message to display upon completion.</param>
    /// <param name="delayMs">The delay in milliseconds between status updates.</param>
    void PrintMessageUponCompletion(string messageBase, string messageUponCompletion, int delayMs = 100);
    /// <summary>
    /// Marks the process as finished and stops the looping status message.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="eventArgs">The event arguments containing the process status.</param>
    void FinishWork(object? sender, IsProcessFinishedEventArgs eventArgs);
}