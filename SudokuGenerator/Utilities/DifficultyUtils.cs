using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Utilities;

/// <summary>
/// Provides utility methods for working with <see cref="Difficulty"/> levels.
/// </summary>
public static class DifficultyUtils
{
    /// <summary>
    /// Attempts to parse an integer into a <see cref="Difficulty"/> value.
    /// </summary>
    /// <param name="number">The integer to parse.</param>
    /// <param name="difficulty">When this method returns, contains the parsed <see cref="Difficulty"/> value if the parsing succeeded; otherwise, the default value of <see cref="Difficulty"/>.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(int number, out Difficulty difficulty)
    {
        if (Enum.IsDefined(typeof(Difficulty), number))
        {
            difficulty = (Difficulty)number;
            return true;
        }
        difficulty = default;
        return false;
    }
    /// <summary>
    /// Retrieves all defined <see cref="Difficulty"/> values.
    /// </summary>
    /// <returns>An enumerable collection of all <see cref="Difficulty"/> values.</returns>
    public static IEnumerable<Difficulty> GetAllDifficulties() =>
        Enum.GetValues<Difficulty>();
}
