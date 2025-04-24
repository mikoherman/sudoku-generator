using Sudoku_Generator.Core.Generators;
using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Factories;

/// <summary>
/// Defines the contract for creating instances of <see cref="ISudokuGenerator"/>
/// configured for specific difficulty levels.
/// </summary>
public interface ISudokuGeneratorFactory
{
    /// <summary>
    /// Creates an instance of <see cref="ISudokuGenerator"/> configured for the specified difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level for which to create the Sudoku generator.</param>
    /// <returns>
    /// An instance of <see cref="ISudokuGenerator"/> configured with the appropriate removal patterns.
    /// </returns>
    ISudokuGenerator CreateSudokuGeneratorFor(Difficulty difficulty);
}