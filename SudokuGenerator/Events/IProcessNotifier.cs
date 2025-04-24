namespace Sudoku_Generator.Events;

/// <summary>
/// Defines the contract for notifying when a process has finished.
/// </summary>
public interface IProcessNotifier
{
    /// <summary>
    /// Raised when the process has finished its work.
    /// </summary>
    event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;
}