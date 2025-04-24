namespace Sudoku_Generator.Core.Validators;

/// <summary>
/// Defines the contract for validating Sudoku boards to ensure they adhere to Sudoku rules.
/// </summary>
public interface ISudokuValidator
{
    /// <summary>
    /// Validates whether the value at the specified row and column in the Sudoku board
    /// complies with Sudoku rules (row, column, and 3x3 grid uniqueness).
    /// </summary>
    /// <param name="row">The row index of the value to validate.</param>
    /// <param name="col">The column index of the value to validate.</param>
    /// <param name="board">The Sudoku board represented as a 2D array.</param>
    /// <returns>
    /// <c>true</c> if the value at the specified position is valid according to Sudoku rules;
    /// otherwise, <c>false</c>.
    /// </returns>
    bool Validate(int row, int col, in int[,] board);
}