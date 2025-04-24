using SudokuGenerator.Models;

namespace Sudoku_Generator.Utilities;

public static class DifficultyUtils
{
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
    public static IEnumerable<Difficulty> GetAllDifficulties() =>
        Enum.GetValues<Difficulty>();
}
