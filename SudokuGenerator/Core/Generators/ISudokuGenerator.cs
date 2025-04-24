using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Core.Generators;

/// <summary>
/// Defines the contract for generating Sudoku puzzles.
/// </summary>
public interface ISudokuGenerator
{
    /// <summary>
    /// Asynchronously generates a specified number of Sudoku puzzles.
    /// </summary>
    /// <param name="boardCount">The number of Sudoku puzzles to generate.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// a collection of <see cref="Sudoku"/> objects, each representing a generated puzzle.
    /// </returns>
    Task<IEnumerable<Sudoku>> GenerateBoardsAsync(int boardCount);
}