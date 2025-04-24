using SudokuGenerator.Models;

namespace Sudoku_Generator.FileHandling;

public interface IPdfFileProcessor
{
    Task CreatePdfsFromSudokusAsync(string solvableFileName, string solutionsFileName, IEnumerable<Sudoku> sudokus);
}