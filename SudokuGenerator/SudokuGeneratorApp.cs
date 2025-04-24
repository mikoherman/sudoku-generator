using Sudoku_Generator.Factories;
using Sudoku_Generator.FileHandling;
using Sudoku_Generator.Core.Generators;
using Sudoku_Generator.Core.Models;
using Sudoku_Generator.UserInteraction;

namespace Sudoku_Generator;

/// <summary>
/// Represents the main application logic for generating Sudoku puzzles and creating PDF files.
/// </summary>
public class SudokuGeneratorApp
{
    private readonly ISudokuGeneratorFactory _generatorFactory;
    private readonly IPdfFileProcessor _pdfFileProcessor;
    private readonly IConsoleUserIOProcessor _userIOProcessor;
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuGeneratorApp"/> class.
    /// </summary>
    /// <param name="generatorFactory">A factory for creating Sudoku generators based on difficulty levels.</param>
    /// <param name="pdfFileProcessor">An instance of <see cref="IPdfFileProcessor"/> to handle PDF creation.</param>
    /// <param name="userIOProcessor">An instance of <see cref="IConsoleUserIOProcessor"/> to handle user input and output.</param>
    public SudokuGeneratorApp(ISudokuGeneratorFactory generatorFactory, 
        IPdfFileProcessor pdfFileProcessor, IConsoleUserIOProcessor userIOProcessor)
    {
        _generatorFactory = generatorFactory;
        _pdfFileProcessor = pdfFileProcessor;
        _userIOProcessor = userIOProcessor;
    }
    /// <summary>
    /// Runs the application to generate Sudoku puzzles and create PDF files.
    /// </summary>
    /// <param name="solvableBoardsFileName">The name of the PDF file containing solvable Sudoku puzzles.</param>
    /// <param name="solutionsFileName">The name of the PDF file containing Sudoku solutions.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Run(string solvableBoardsFileName, string solutionsFileName)
    {
        int numberOfSudokuBoards = _userIOProcessor.PromptUserForNumber();
        Difficulty sudokuDifficulty = _userIOProcessor.PromptUserForDifficulty();
        ISudokuGenerator sudokuGenerator = _generatorFactory
            .CreateSudokuGeneratorFor(sudokuDifficulty);
        _userIOProcessor.DisplaySudokuGeneratingStatus();
        IEnumerable<Sudoku> sudokuBoards = await sudokuGenerator
            .GenerateBoardsAsync(numberOfSudokuBoards);
        _userIOProcessor.DisplayPdfProcessingStatus();
        await _pdfFileProcessor
            .CreatePdfsFromSudokusAsync(solvableBoardsFileName, solutionsFileName, sudokuBoards);
    }
}