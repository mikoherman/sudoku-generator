using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Core.Generators;

public interface ISudokuGenerator
{
    Task<IEnumerable<Sudoku>> GenerateBoardsAsync(int boardCount);
}