namespace Sudoku_Generator.Events;

/// <summary>
/// Provides data for the <see cref="IProcessNotifier.ProcessFinished"/> event.
/// </summary>
public class IsProcessFinishedEventArgs : EventArgs {
    /// <summary>
    /// Gets a value indicating whether the process has finished.
    /// </summary>
    public bool IsProcessFinished { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="IsProcessFinishedEventArgs"/> class.
    /// </summary>
    /// <param name="isProcessFinished">A value indicating whether the process has finished.</param>
    public IsProcessFinishedEventArgs(bool isProcessFinished)
    {
        IsProcessFinished = isProcessFinished;
    }
}