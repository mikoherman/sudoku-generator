using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Utilities;

namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Handles user input and output operations for the Sudoku application.
/// </summary>
public class ConsoleUserIOProcessor : IConsoleUserIOProcessor
{
    private readonly IConsoleUserInteractor _userInteractor;
    private readonly ILoopingStatusPrinter _pdfProcessingStatusPrinter;
    private readonly ILoopingStatusPrinter _sudokuGeneratingStatusPrinter;
    private readonly string _separator = Environment.NewLine;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleUserIOProcessor"/> class.
    /// </summary>
    /// <param name="userInteractor">An instance of <see cref="IConsoleUserInteractor"/> for console interactions.</param>
    /// <param name="pdfProcessingStatusPrinter">An instance of <see cref="ILoopingStatusPrinter"/> for PDF processing status updates.</param>
    /// <param name="sudokuGeneratingStatusPrinter">An instance of <see cref="ILoopingStatusPrinter"/> for Sudoku generation status updates.</param>
    public ConsoleUserIOProcessor(IConsoleUserInteractor userInteractor, 
        ILoopingStatusPrinter pdfProcessingStatusPrinter, 
        ILoopingStatusPrinter sudokuGeneratingStatusPrinter)
    {
        _userInteractor = userInteractor;
        _pdfProcessingStatusPrinter = pdfProcessingStatusPrinter;
        _sudokuGeneratingStatusPrinter = sudokuGeneratingStatusPrinter;
    }
    /// <summary>
    /// Prompts the user to input the number of Sudoku boards to generate.
    /// </summary>
    /// <returns>The number of Sudoku boards to generate.</returns>
    public int PromptUserForNumber()
    {
        string? userInput;
        int number;
        do
        {
            _userInteractor.ShowMessage($"Please input number of sudoku boards to generate:");
            userInput = _userInteractor.Read();
        } while (string.IsNullOrEmpty(userInput) ||
        !int.TryParse(userInput, out number));
        return number;
    }
    /// <summary>
    /// Displays the generated Sudoku boards and their solutions.
    /// </summary>
    /// <param name="sudokus">A collection of <see cref="Sudoku"/> objects to display.</param>
    public void DisplaySudokuBoards(IEnumerable<Sudoku> sudokus)
    {
        int counter = 1;
        _userInteractor.ShowMessage(string.Join(_separator, sudokus.Select(sudoku =>
        {
            return $"Sudoku number {counter++}{_separator}" +
            $"{sudoku.SolvableBoard.ToFormattedSudokuString()}{_separator}" +
            $"Solution: {_separator}{sudoku.Solution.ToFormattedSudokuString()}";
        })));
    }
    /// <summary>
    /// Displays the available difficulty levels to the user.
    /// </summary>
    public void DisplayDifficulties()
    {
        var allDifficultiesAsString = DifficultyUtils
            .GetAllDifficulties()
            .Select(difficulty => $"{(int)difficulty}. {difficulty}");
        _userInteractor.ShowMessage($"Difficulty levels:{_separator}" +
            $"{string.Join(_separator, allDifficultiesAsString)}");
    }
    /// <summary>
    /// Prompts the user to select a difficulty level.
    /// </summary>
    /// <returns>The selected <see cref="Difficulty"/> level.</returns>
    public Difficulty PromptUserForDifficulty()
    {
        string? userInput;
        Difficulty difficulty;
        do
        {
            _userInteractor.ShowMessage($"Choose difficulty:");
            userInput = _userInteractor.Read();
        } while (string.IsNullOrEmpty(userInput) ||
        !int.TryParse(userInput, out int number) || 
        !DifficultyUtils.TryParse(number, out difficulty));
        return difficulty;
    }
    /// <summary>
    /// Displays the status of the PDF processing operation.
    /// </summary>
    public void DisplayPdfProcessingStatus() =>
        _pdfProcessingStatusPrinter.PrintMessageUponCompletion("Generating Pdfs", "Pdfs have been generated");
    /// <summary>
    /// Displays the status of the Sudoku generation operation.
    /// </summary>
    public void DisplaySudokuGeneratingStatus() =>
        _sudokuGeneratingStatusPrinter.PrintMessageUponCompletion("Generating Sudokus", "Sudokus have been generated");
}
