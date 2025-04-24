using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Core.RemovalPatterns;

/// <summary>
/// Defines the contract for implementing Sudoku removal patterns.
/// </summary>
public interface IRemovalPattern
{
    /// <summary>
    /// Converts a fully solved Sudoku board into a puzzle by removing numbers
    /// based on the specific removal pattern.
    /// </summary>
    /// <param name="board">The fully solved Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// A <see cref="Sudoku"/> object containing the puzzle board and its solution.
    /// </returns>
    Sudoku ConvertBoardToSudoku(int[,] board);
}
