using System.Text;

namespace Sudoku_Generator.Utilities;

/// <summary>
/// Provides extension methods for working with Sudoku grids.
/// </summary>
public static class GridExtensions
{
    /// <summary>
    /// Converts a 9x9 Sudoku grid into a formatted string representation.
    /// </summary>
    /// <param name="board">The 9x9 Sudoku grid to format.</param>
    /// <returns>A formatted string representation of the Sudoku grid.</returns>
    public static string ToFormattedSudokuString(this int[,] board)
    {
        var sb = new StringBuilder();
        sb.Length = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sb.Append(board[i, j] + " ");
                if (j != 8 && (j + 1) % 3 == 0)
                    sb.Append("| ");
            }
            sb.AppendLine();
            if (i != 8 && (i + 1) % 3 == 0)
                sb.AppendLine(new string('-', 21));
        }
        return sb.ToString();
    }
}