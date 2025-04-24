using Sudoku_Generator.Events;

namespace Sudoku_Generator.UserInteraction;

public interface ILoopingStatusPrinter
{
    void PrintMessageUponCompletion(string messageBase, string messageUponCompletion, int delayMs = 100);
    void FinishWork(object? sender, IsProcessFinishedEventArgs eventArgs);
}