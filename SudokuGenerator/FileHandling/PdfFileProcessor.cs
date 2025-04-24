using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Events;

namespace Sudoku_Generator.FileHandling;

public class PdfFileProcessor : IPdfFileProcessor, IProcessNotifier
{
    private readonly ISudokuPdfHandler _pdfHandler;
    public event EventHandler<IsProcessFinishedEventArgs>? ProcessFinished;

    public PdfFileProcessor(ISudokuPdfHandler pdfHandler)
    {
        _pdfHandler = pdfHandler;
    }

    public async Task CreatePdfsFromSudokusAsync(string solvableFileName, string solutionsFileName, IEnumerable<Sudoku> sudokus)
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
        OnProcessFinished();
    }

    private void OnProcessFinished() =>
        ProcessFinished?.Invoke(
            this,
            new IsProcessFinishedEventArgs(true));
}
