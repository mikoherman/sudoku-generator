using SudokuGenerator.Models;

namespace SudokuGenerator.RemovalPatterns;

public class CheckboardPatternRemoval : IRemovalPattern
{
    private readonly Dictionary<int, Func<int, bool>> _typeOfRemovalSelector =
        new ()
        {
            // even
            [0] = (col) =>
            {
                if (col % 2 == 0)
                    return true;
                return false;
            },
            // odd
            [1] = (col) =>
            {
                if (col % 2 == 1)
                    return true;
                return false;
            },
            // special pattern like:
            // 5 * 6 | * * * | 3 * 4
            [2] = (col) =>
            {
                switch (col)
                {
                    case int n when
                            n == 1 ||
                            n >= 3 && n <= 5 ||
                            n == 7:
                        return true;
                    default:
                        return false;
                }
            }
        };
    private readonly Random _rand;

    public CheckboardPatternRemoval(Random rand)
    {
        _rand = rand;
    }

    public Sudoku ConvertBoardToSudoku(int[,] board)
    {
        var solution = (int[,])board.Clone();
        int row = 0;
        int? previousIndex = null;
        while (row < board.GetLength(0))
        {
            int typeOfRemovalIndex = _rand.Next(3);
            while (previousIndex.HasValue && previousIndex.Value == typeOfRemovalIndex)
                typeOfRemovalIndex = _rand.Next(3);
            var removalSelector = _typeOfRemovalSelector[typeOfRemovalIndex];
            previousIndex = typeOfRemovalIndex;
            for (int col = 0; col < board.GetLength(0); col++)
            {
                if (removalSelector(col))
                    board[row, col] = 0;
            }
            row++;
        }
        return new Sudoku(board, solution);
    }
}