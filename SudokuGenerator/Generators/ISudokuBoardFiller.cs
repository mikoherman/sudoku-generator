namespace SudokuGenerator.Generators;

public interface ISudokuBoardFiller
{
    int[,] GenerateValidSudokuGrid();
}