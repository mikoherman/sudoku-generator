using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;

namespace Sudoku_Generator.Factories;

public interface IRemovalPatternsFactory
{
    IList<IRemovalPattern> GetRemovalPatternsFor(Difficulty difficulty);
}