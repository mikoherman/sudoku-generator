namespace Sudoku_Generator.Core.Validators;

/// <summary>
/// Provides functionality to validate Sudoku boards and ensure they adhere to Sudoku rules.
/// </summary>
public class SudokuValidator : ISudokuValidator
{
    /// <summary>
    /// Validates whether the value at the specified row and column in the Sudoku board
    /// adheres to Sudoku rules (row, column, and 3x3 grid uniqueness).
    /// Designed to iterate over current row, column and 3x3 grid in one loop.
    /// </summary>
    /// <param name="row">The row index of the value to validate.</param>
    /// <param name="col">The column index of the value to validate.</param>
    /// <param name="board">The Sudoku board represented as a 2D array.</param>
    /// <returns>
    /// <c>true</c> if the value at the specified position is valid according to Sudoku rules;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool Validate(int row, int col, in int[,] board)
    {
        int valueToCheck = board[row, col];
        // helper variables for 3x3 grid calculation
        int boxColStartPoint = col / 3 * 3;
        int boxRowStartPoint = row / 3 * 3;
        int boxColCurrent;
        int boxRowCurrent;
        int boardSize = board.GetLength(0);
        for (int i = 0; i < boardSize; i++)
        {
            if (i != col && board[row, i] == valueToCheck)
                return false;
            if (i != row && board[i, col] == valueToCheck)
                return false;
            // calculate 3x3 current iteration
            boxColCurrent = boxColStartPoint + i % 3;
            boxRowCurrent = boxRowStartPoint + i / 3;
            if (!(boxColCurrent == col && boxRowCurrent == row) &&
                board[boxRowCurrent, boxColCurrent] == valueToCheck)
                return false;
        }
        return true;
    }
}