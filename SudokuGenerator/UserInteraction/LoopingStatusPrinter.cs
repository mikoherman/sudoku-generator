using Sudoku_Generator.Events;

namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Provides functionality to display looping status messages until a process is completed.
/// </summary>
public class LoopingStatusPrinter : ILoopingStatusPrinter
{
    private bool IsProcessFinished { get; set; } = false;
    private readonly IConsoleUserInteractor _userInteractor;
    /// <summary>
    /// Initializes a new instance of the <see cref="LoopingStatusPrinter"/> class.
    /// </summary>
    /// <param name="userInteractor">An instance of <see cref="IConsoleUserInteractor"/> for console interactions.</param>
    public LoopingStatusPrinter(IConsoleUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }
    /// <summary>
    /// Displays a looping status message until the process is completed.
    /// </summary>
    /// <param name="messageBase">The base message to display.</param>
    /// <param name="messageUponCompletion">The message to display upon completion.</param>
    /// <param name="delayMs">The delay in milliseconds between status updates.</param>
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
    /// <summary>
    /// Marks the process as finished and stops the looping status message.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="eventArgs">The event arguments containing the process status.</param>
    public void FinishWork(object? sender, IsProcessFinishedEventArgs eventArgs) =>
        IsProcessFinished = eventArgs.IsProcessFinished;
}