using SudokuGenerator.Generators;
using SudokuGenerator.Models;

namespace Sudoku_Generator.Factories;

public interface ISudokuGeneratorFactory
{
    ISudokuGenerator CreateSudokuGeneratorFor(Difficulty difficulty);
}