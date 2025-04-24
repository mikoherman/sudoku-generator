using Serilog;
using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Events;

namespace Sudoku_Generator.FileHandling;

/// <summary>
/// Handles the creation of PDF files for Sudoku puzzles and their solutions.
/// </summary>
public class PdfFileProcessor : IPdfFileProcessor, IProcessNotifier
{
    /// <summary>
    /// Occurs when the PDF creation process is finished.
    /// </summary>
    public event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;
    private readonly ISudokuPdfHandler _pdfHandler;
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfFileProcessor"/> class.
    /// </summary>
    /// <param name="pdfHandler">An instance of <see cref="ISudokuPdfHandler"/> to handle PDF creation.</param>
    public PdfFileProcessor(ISudokuPdfHandler pdfHandler)
    {
        _pdfHandler = pdfHandler;
    }
    /// <summary>
    /// Asynchronously creates PDF files for Sudoku puzzles and their solutions.
    /// </summary>
    /// <param name="solvableFileName">The name of the PDF file containing solvable Sudoku puzzles.</param>
    /// <param name="solutionsFileName">The name of the PDF file containing Sudoku solutions.</param>
    /// <param name="sudokus">A collection of <see cref="Sudoku"/> objects to include in the PDFs.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreatePdfsFromSudokusAsync(string solvableFileName, string solutionsFileName, IEnumerable<Sudoku> sudokus)
    {
        try
        {
            var tasks = new List<Task> {
            Task.Run(() =>
            _pdfHandler.CreatePdf(
                solvableFileName,
                "Sudoku",
                sudokus.Select(sudoku => sudoku.SolvableBoard))),
            Task.Run(() =>
            _pdfHandler.CreatePdf(
                solutionsFileName,
                "Solutions",
                sudokus.Select(sudoku => sudoku.Solution)))
            };
            await Task.WhenAll(tasks);
        }catch(Exception ex)
        {
            Log.Error($"An error has occured when generating one of the PDFs: {ex}");
            throw;
        }
        finally
        {
            OnProcessFinished();
        }
    }
    /// <summary>
    /// Invokes the <see cref="ProcessFinished"/> event to notify that the PDF creation process is complete.
    /// </summary>
    private void OnProcessFinished() =>
        ProcessFinished?.Invoke(
            this,
            new IsProcessFinishedEventArgs(true));
}
