namespace SudokuGenerator.UserInteraction;

public class ConsoleUserInteractor : IConsoleUserInteractor
{
    public string? Read() =>
        Console.ReadLine();
    public void ShowMessage(string message) =>
        Console.WriteLine(message);
    public void Clear() =>
        Console.Clear();
}
