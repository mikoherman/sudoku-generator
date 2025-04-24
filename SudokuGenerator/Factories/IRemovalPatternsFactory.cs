using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;

namespace Sudoku_Generator.Factories;

/// <summary>
/// Defines the contract for retrieving removal patterns for Sudoku puzzles
/// based on the specified difficulty level.
/// </summary>
public interface IRemovalPatternsFactory
{
    /// <summary>
    /// Retrieves the list of removal patterns associated with the specified difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level for which to retrieve removal patterns.</param>
    /// <returns>
    /// A list of <see cref="IRemovalPattern"/> objects associated with the specified difficulty level.
    /// </returns>
    IList<IRemovalPattern> GetRemovalPatternsFor(Difficulty difficulty);
}