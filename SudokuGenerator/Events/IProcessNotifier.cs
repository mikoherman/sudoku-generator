namespace Sudoku_Generator.Events;

public interface IProcessNotifier
{
    /// <summary>
    /// Raised when the process has finished its work.
    /// </summary>
    event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;
}