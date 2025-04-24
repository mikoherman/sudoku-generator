namespace SudokuGenerator.UserInteraction;

public class ConsoleUserInteractor : IConsoleUserInteractor
{
    public string? Read()
    {
        return Console.ReadLine();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
