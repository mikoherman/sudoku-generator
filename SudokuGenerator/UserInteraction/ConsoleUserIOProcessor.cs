using Sudoku_Generator.Utilities;
using SudokuGenerator.Models;

namespace SudokuGenerator.UserInteraction;

public class ConsoleUserIOProcessor : IConsoleUserIOProcessor
{
    private readonly IConsoleUserInteractor _userInteractor;
    private readonly string _separator = Environment.NewLine;

    public ConsoleUserIOProcessor(IConsoleUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }
    public int PromptUserForNumber()
    {
        string? userInput;
        int number;
        do
        {
            _userInteractor.ShowMessage($"Please input number of sudoku boards to generate:{_separator}");
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
            $"{GridExtensions.ToFormattedSudokuString(sudoku.SolvableBoard)}{_separator}" +
            $"Solution: {_separator}{GridExtensions.ToFormattedSudokuString(sudoku.Solution)}";
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
            _userInteractor.ShowMessage($"Choose difficulty: {_separator}");
            userInput = _userInteractor.Read();
        } while (string.IsNullOrEmpty(userInput) ||
        !int.TryParse(userInput, out int number) || 
        !DifficultyUtils.TryParse(number, out difficulty));
        return difficulty;
    }
}
