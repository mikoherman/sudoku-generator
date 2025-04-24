using System.Text;

namespace Sudoku_Generator.Utilities;

public static class GridExtensions
{
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


