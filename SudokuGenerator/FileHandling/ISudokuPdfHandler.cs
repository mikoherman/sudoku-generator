
namespace Sudoku_Generator.FileHandling;

/// <summary>
/// Defines the contract for creating PDF files containing Sudoku puzzles and their solutions.
/// </summary>
public interface ISudokuPdfHandler
{
    /// <summary>
    /// Creates a PDF file containing Sudoku puzzles or solutions.
    /// </summary>
    /// <param name="filename">The name of the PDF file to create.</param>
    /// <param name="mainTitle">The main title to display at the top of the PDF.</param>
    /// <param name="sudokuBoards">A collection of Sudoku boards to include in the PDF.</param>
    void CreatePdf(string filename, string mainTitle, IEnumerable<int[,]> sudokuBoards);
}