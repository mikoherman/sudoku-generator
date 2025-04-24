using SudokuGenerator.Models;
using SudokuGenerator.RemovalPatterns;

namespace Sudoku_Generator.Factories;

public interface IRemovalPatternsFactory
{
    IList<IRemovalPattern> GetRemovalPatternsFor(Difficulty difficulty);
}