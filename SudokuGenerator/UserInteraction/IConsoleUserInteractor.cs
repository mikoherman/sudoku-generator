namespace Sudoku_Generator.UserInteraction;

public interface IConsoleUserInteractor
{
    string? Read();
    void ShowMessage(string message);
    void Clear();
}