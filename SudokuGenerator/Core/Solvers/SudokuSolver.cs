using Sudoku_Generator.Core.Validators;

namespace Sudoku_Generator.Core.Solvers;

/// <summary>
/// Provides functionality to solve Sudoku puzzles using a backtracking algorithm.
/// </summary>
public class SudokuSolver : ISudokuSolver
{
    private readonly ISudokuValidator _validator;
    private readonly Random _rand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuSolver"/> class.
    /// </summary>
    /// <param name="validator">An instance of <see cref="ISudokuValidator"/> to validate board states.</param>
    /// <param name="rand">A random number generator for selecting numbers during solving.</param>
    public SudokuSolver(ISudokuValidator validator, Random rand)
    {
        _validator = validator;
        _rand = rand;
    }

    /// <summary>
    /// Attempts to solve the given Sudoku board.
    /// </summary>
    /// <param name="board">The Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// <c>true</c> if the board is successfully solved; otherwise, <c>false</c>.
    /// </returns>
    public bool Solve(int[,] board) =>
        Solve(0, 0, board);

    /// <summary>
    /// Recursively solves the Sudoku board using a backtracking algorithm.
    /// </summary>
    /// <param name="row">The current row index being processed.</param>
    /// <param name="col">The current column index being processed.</param>
    /// <param name="board">The Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// <c>true</c> if the board is successfully solved; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method implements a recursive backtracking algorithm to solve the Sudoku board.
    /// It generates a shuffled list of possible numbers for the current empty cell,
    /// tests each one for validity using the provided validator, and recursively proceeds to the next cell.
    /// If no valid number can be placed, the algorithm backtracks to try a different number in a previous cell.
    /// </remarks>
    private bool Solve(int row, int col, int[,] board)
    {
        // terminates, solution is found
        if (row == 9)
            return true;
        // goes to the next row
        else if (col == 9)
            return Solve(row + 1, 0, board);
        // there is a number in this cell, thus goes to the next column
        else if (board[row, col] != 0)
            return Solve(row, col + 1, board);
        else
        {
            var possibleNumbers = Enumerable.Range(1, 9).ToList();
            while (possibleNumbers.Count > 0)
            {
                // randomly choses number in range 1-9
                int index = _rand.Next(possibleNumbers.Count);
                int numberToTest = possibleNumbers[index];
                // removes it from the list of numbers
                possibleNumbers.RemoveAt(index);
                // applies the number to the board
                board[row, col] = numberToTest;
                // tests for valid board
                if (_validator.Validate(row, col, board))
                {
                    // goes to next column and tries to solve
                    if (Solve(row, col + 1, board))
                        return true;
                }
                // remove the number if it leads to a dead end
                board[row, col] = 0;
            }
            // No valid number could be placed in this cell — backtrack
            return false;
        }
    }
}


