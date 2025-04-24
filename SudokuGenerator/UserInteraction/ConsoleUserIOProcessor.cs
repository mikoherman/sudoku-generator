using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Utilities;

namespace Sudoku_Generator.UserInteraction;

public class ConsoleUserIOProcessor : IConsoleUserIOProcessor
{
    private readonly IConsoleUserInteractor _userInteractor;
    private readonly ILoopingStatusPrinter _pdfProcessingStatusPrinter;
    private readonly ILoopingStatusPrinter _sudokuGeneratingStatusPrinter;
    private readonly string _separator = Environment.NewLine;
    public ConsoleUserIOProcessor(IConsoleUserInteractor userInteractor, 
        ILoopingStatusPrinter pdfProcessingStatusPrinter, 
        ILoopingStatusPrinter sudokuGeneratingStatusPrinter)
    {
        _userInteractor = userInteractor;
        _pdfProcessingStatusPrinter = pdfProcessingStatusPrinter;
        _sudokuGeneratingStatusPrinter = sudokuGeneratingStatusPrinter;
    }

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

    public void DisplayDifficulties()
    {
        var allDifficultiesAsString = DifficultyUtils
            .GetAllDifficulties()
            .Select(difficulty => $"{(int)difficulty}. {difficulty}");
        _userInteractor.ShowMessage($"Difficulty levels:{_separator}" +
            $"{string.Join(_separator, allDifficultiesAsString)}");
    }
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
    public void DisplayPdfProcessingStatus() =>
        _pdfProcessingStatusPrinter.PrintMessageUponCompletion("Generating Pdfs", "Pdfs have been generated");
    public void DisplaySudokuGeneratingStatus() =>
        _sudokuGeneratingStatusPrinter.PrintMessageUponCompletion("Generating Sudokus", "Sudokus have been generated");
}
