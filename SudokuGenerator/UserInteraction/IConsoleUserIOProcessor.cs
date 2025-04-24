using SudokuGenerator.Models;

namespace SudokuGenerator.UserInteraction;

public interface IConsoleUserIOProcessor
{
    void DisplaySudokuBoards(IEnumerable<Sudoku> sudokus);
    int PromptUserForNumber();
    Difficulty PromptUserForDifficulty();
    void DisplayDifficulties();
    void DisplaySudokuGeneratingStatus();
    void DisplayPdfProcessingStatus();
}