namespace Sudoku_Generator.Core.Generators;

/// <summary>
/// Defines the contract for generating a valid, fully solved Sudoku grid.
/// </summary>
public interface ISudokuBoardFiller
{
    /// <summary>
    /// Generates a valid, fully solved 9x9 Sudoku grid.
    /// </summary>
    /// <returns>
    /// A 9x9 2D array representing a fully solved Sudoku grid.
    /// </returns>
    int[,] GenerateValidSudokuGrid();
}