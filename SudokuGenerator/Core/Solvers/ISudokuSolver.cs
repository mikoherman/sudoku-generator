namespace Sudoku_Generator.Core.Solvers;

/// <summary>
/// Defines the contract for solving Sudoku puzzles.
/// </summary>
public interface ISudokuSolver
{
    /// <summary>
    /// Attempts to solve the given Sudoku board.
    /// </summary>
    /// <param name="board">The Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// <c>true</c> if the board is successfully solved; otherwise, <c>false</c>.
    /// </returns>
    bool Solve(int[,] board);
}