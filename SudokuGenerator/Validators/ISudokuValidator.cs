namespace SudokuGenerator.Validators;

public interface ISudokuValidator
{
    bool Validate(int row, int col, in int[,] board);
}