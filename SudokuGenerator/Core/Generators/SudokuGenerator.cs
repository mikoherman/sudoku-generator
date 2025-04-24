using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Events;

namespace Sudoku_Generator.Core.Generators;

public class SudokuGenerator : ISudokuGenerator, IProcessNotifier
{
    public event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;
    private readonly ISudokuBoardFiller _boardFiller;
    private readonly IList<IRemovalPattern> _removalPatterns;
    private readonly Random _rand;

    public SudokuGenerator(ISudokuBoardFiller boardFiller,
        IList<IRemovalPattern> removalPatterns, Random rand)
    {
        _boardFiller = boardFiller;
        _removalPatterns = removalPatterns;
        _rand = rand;
    }

    public async Task<IEnumerable<Sudoku>> GenerateBoardsAsync(int boardCount)
    {
        var tasks = new List<Task<Sudoku>>(boardCount);
        while (boardCount > 0)
        {
            tasks.Add(Task.Run(() =>
            {
                int[,] sudokuBoard = _boardFiller.GenerateValidSudokuGrid();
                IRemovalPattern removalPattern = _removalPatterns[_rand.Next(_removalPatterns.Count())];
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

    private void OnProcessFinished() =>
        ProcessFinished?.Invoke(this, 
            new IsProcessFinishedEventArgs(true));
}
