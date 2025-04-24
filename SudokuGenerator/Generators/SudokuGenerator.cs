using SudokuGenerator.Models;
using SudokuGenerator.RemovalPatterns;

namespace SudokuGenerator.Generators;

public class SudokuGenerator : ISudokuGenerator
{
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

    public async Task<IEnumerable<Sudoku>> GenerateBoards(int boardCount)
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
        return await Task.WhenAll(tasks);
    }
}
