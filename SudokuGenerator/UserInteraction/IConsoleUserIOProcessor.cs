using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.UserInteraction;

/// <summary>
/// Defines the contract for handling user input and output operations in the Sudoku application.
/// </summary>
public interface IConsoleUserIOProcessor
{
    /// <summary>
    /// Displays the generated Sudoku boards and their solutions.
    /// </summary>
    /// <param name="sudokus">A collection of <see cref="Sudoku"/> objects to display.</param>
    void DisplaySudokuBoards(IEnumerable<Sudoku> sudokus);

    /// <summary>
    /// Prompts the user to input the number of Sudoku boards to generate.
    /// </summary>
    /// <returns>The number of Sudoku boards to generate.</returns>
    int PromptUserForNumber();

    /// <summary>
    /// Prompts the user to select a difficulty level.
    /// </summary>
    /// <returns>The selected <see cref="Difficulty"/> level.</returns>
    Difficulty PromptUserForDifficulty();

    /// <summary>
    /// Displays the status of the Sudoku generation operation.
    /// </summary>
    void DisplaySudokuGeneratingStatus();

    /// <summary>
    /// Displays the status of the PDF processing operation.
    /// </summary>
    void DisplayPdfProcessingStatus();
}
