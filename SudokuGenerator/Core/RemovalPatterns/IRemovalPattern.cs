using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Core.RemovalPatterns;

public interface IRemovalPattern
{
    Sudoku ConvertBoardToSudoku(int[,] board);
}
