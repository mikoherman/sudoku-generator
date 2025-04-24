using SudokuGenerator.Models;

namespace SudokuGenerator.RemovalPatterns;

public interface IRemovalPattern
{
    Sudoku ConvertBoardToSudoku(int[,] board);
}
