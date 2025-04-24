using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.FileHandling;

/// <summary>
/// Defines the contract for processing and creating PDF files for Sudoku puzzles and their solutions.
/// </summary>
public interface IPdfFileProcessor
{
    /// <summary>
    /// Asynchronously creates PDF files for Sudoku puzzles and their solutions.
    /// </summary>
    /// <param name="solvableFileName">The name of the PDF file containing solvable Sudoku puzzles.</param>
    /// <param name="solutionsFileName">The name of the PDF file containing Sudoku solutions.</param>
    /// <param name="sudokus">A collection of <see cref="Sudoku"/> objects to include in the PDFs.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreatePdfsFromSudokusAsync(string solvableFileName, string solutionsFileName, IEnumerable<Sudoku> sudokus);
}