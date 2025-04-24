namespace Sudoku_Generator.Core.Generators;

public interface ISudokuBoardFiller
{
    int[,] GenerateValidSudokuGrid();
}