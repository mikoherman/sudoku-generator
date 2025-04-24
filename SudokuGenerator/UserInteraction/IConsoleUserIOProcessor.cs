using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.UserInteraction;

public interface IConsoleUserIOProcessor
{
    void DisplaySudokuBoards(IEnumerable<Sudoku> sudokus);
    int PromptUserForNumber();
    Difficulty PromptUserForDifficulty();
    void DisplayDifficulties();
    void DisplaySudokuGeneratingStatus();
    void DisplayPdfProcessingStatus();
}