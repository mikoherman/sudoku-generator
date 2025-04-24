using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator.Core.Generators;

/// <summary>
/// Provides functionality to generate a valid, fully solved Sudoku grid.
/// </summary>
public class SudokuBoardFiller : ISudokuBoardFiller
{
    private readonly ISudokuSolver _solver;

    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuBoardFiller"/> class.
    /// </summary>
    /// <param name="solver">An instance of <see cref="ISudokuSolver"/> used to solve the Sudoku boards </param>
    public SudokuBoardFiller(ISudokuSolver solver)
    {
        _solver = solver;
    }
    /// <summary>
    /// Generates a valid, fully solved 9x9 Sudoku grid.
    /// </summary>
    /// <returns>
    /// A 9x9 2D array representing a fully solved Sudoku grid.
    /// </returns>
    /// <remarks>
    /// The method initializes an empty Sudoku board and uses the provided <see cref="ISudokuSolver"/>
    /// to solve it, ensuring the generated grid is valid.
    /// </remarks>
    public int[,] GenerateValidSudokuGrid()
    {
        int[,] sudokuBoard;
        do
        {
            sudokuBoard = new int[9, 9];
        } while (!_solver.Solve(sudokuBoard));
        return sudokuBoard;
    }
}


