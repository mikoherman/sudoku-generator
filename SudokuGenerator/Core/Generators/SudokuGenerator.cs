using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Events;

namespace Sudoku_Generator.Core.Generators;

/// <summary>
/// Generates Sudoku puzzles by filling a valid Sudoku grid and applying removal patterns
/// to create puzzles of varying difficulty.
/// </summary>
public class SudokuGenerator : ISudokuGenerator, IProcessNotifier
{
    /// <summary>
    /// Event triggered when the board generation process is finished.
    /// </summary>
    public event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;
    private readonly ISudokuBoardFiller _boardFiller;
    private readonly IList<IRemovalPattern> _removalPatterns;
    private readonly Random _rand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuGenerator"/> class.
    /// </summary>
    /// <param name="boardFiller">An instance of <see cref="ISudokuBoardFiller"/> to generate valid Sudoku grids.</param>
    /// <param name="removalPatterns">A list of removal patterns to apply to the Sudoku grid.</param>
    /// <param name="rand">A random number generator for selecting removal patterns.</param>
    public SudokuGenerator(ISudokuBoardFiller boardFiller,
        IList<IRemovalPattern> removalPatterns, Random rand)
    {
        _boardFiller = boardFiller;
        _removalPatterns = removalPatterns;
        _rand = rand;
    }

    /// <summary>
    /// Asynchronously generates a specified number of Sudoku puzzles.
    /// </summary>
    /// <param name="boardCount">The number of Sudoku puzzles to generate.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// a collection of <see cref="Sudoku"/> objects, each representing a generated puzzle.
    /// </returns>
    public async Task<IEnumerable<Sudoku>> GenerateBoardsAsync(int boardCount)
    {
        var tasks = new List<Task<Sudoku>>(boardCount);
        while (boardCount > 0)
        {
            tasks.Add(Task.Run(() =>
            {
                int[,] sudokuBoard = _boardFiller.GenerateValidSudokuGrid();
                IRemovalPattern removalPattern = _removalPatterns[_rand.Next(_removalPatterns.Count)];
                return removalPattern.ConvertBoardToSudoku(sudokuBoard);
            }));
            boardCount--;
        }
        return await Task.WhenAll(tasks).ContinueWith(task =>
        {
            OnProcessFinished();
            return task.Result;
        });
    }
    /// <summary>
    /// Invokes the <see cref="ProcessFinished"/> event to notify that the generation process is complete.
    /// </summary>
    private void OnProcessFinished() =>
        ProcessFinished?.Invoke(this, 
            new IsProcessFinishedEventArgs(true));
}
