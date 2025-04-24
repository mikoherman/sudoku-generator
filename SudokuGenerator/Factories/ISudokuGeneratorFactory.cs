using Sudoku_Generator.Core.Generators;
using Sudoku_Generator.Core.Models;

namespace Sudoku_Generator.Factories;

public interface ISudokuGeneratorFactory
{
    ISudokuGenerator CreateSudokuGeneratorFor(Difficulty difficulty);
}