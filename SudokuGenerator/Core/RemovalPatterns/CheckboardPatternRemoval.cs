using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Core.RemovalPatterns;

/// <summary>
/// Implements a checkerboard-like removal pattern for generating Sudoku puzzles
/// by selectively removing numbers from a fully solved board.
/// </summary>
public class CheckboardPatternRemoval : IRemovalPattern
{
    private readonly Dictionary<int, Func<int, bool>> _typeOfRemovalSelector =
        new ()
        {
            // remove even
            [0] = (col) =>
            {
                if (col % 2 == 0)
                    return true;
                return false;
            },
            // remove odd
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

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckboardPatternRemoval"/> class.
    /// </summary>
    /// <param name="rand">A random number generator used to select the type of removal pattern.</param>
    public CheckboardPatternRemoval(Random rand)
    {
        _rand = rand;
    }
    /// <summary>
    /// Converts a fully solved Sudoku board into a puzzle by removing numbers
    /// based on a checkerboard-like pattern.
    /// </summary>
    /// <param name="board">The fully solved Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// A <see cref="Sudoku"/> object containing the puzzle board and its solution.
    /// </returns>
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