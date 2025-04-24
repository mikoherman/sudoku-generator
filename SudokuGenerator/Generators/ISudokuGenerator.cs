using SudokuGenerator.Models;

namespace SudokuGenerator.Generators;

public interface ISudokuGenerator
{
    Task<IEnumerable<Sudoku>> GenerateBoardsAsync(int boardCount);
}