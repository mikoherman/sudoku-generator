namespace Sudoku_Generator.Events;

public class IsProcessFinishedEventArgs : EventArgs {
    public bool IsProcessFinished { get; }
    public IsProcessFinishedEventArgs(bool isProcessFinished)
    {
        IsProcessFinished = isProcessFinished;
    }
}