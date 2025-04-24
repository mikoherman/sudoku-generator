using Sudoku_Generator.Events;

namespace Sudoku_Generator.UserInteraction;

public class LoopingStatusPrinter : ILoopingStatusPrinter
{
    private bool IsProcessFinished { get; set; } = false;
    private readonly IConsoleUserInteractor _userInteractor;
    public LoopingStatusPrinter(IConsoleUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }
    public void PrintMessageUponCompletion(string messageBase,
        string messageUponCompletion,
        int delayMs = 100)
    {
        var messagesList = new[]
        {
            messageBase,
            $"{messageBase} .",
            $"{messageBase} ..",
            $"{messageBase} ..."
        };
        Task.Run(async () =>
        {
            while (!IsProcessFinished)
            {
                foreach (var message in messagesList)
                {
                    if (!IsProcessFinished)
                    {
                        _userInteractor.Clear();
                        _userInteractor.ShowMessage(message);
                        await Task.Delay(delayMs);
                    }
                }
            }
            _userInteractor.ShowMessage(messageUponCompletion);
        });
    }
    public void FinishWork(object? sender, IsProcessFinishedEventArgs eventArgs) =>
        IsProcessFinished = eventArgs.IsProcessFinished;
}
