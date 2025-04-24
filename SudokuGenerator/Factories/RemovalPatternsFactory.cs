using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Core.Solvers;
using SudokuGenerator.RemovalPatterns;

namespace Sudoku_Generator.Factories;

public class RemovalPatternsFactory : IRemovalPatternsFactory
{
    private readonly Random _rand;
    private readonly ISudokuSolver _solver;
    private readonly Dictionary<Difficulty, IList<IRemovalPattern>> _difficultyToRemovalMapper;

    public RemovalPatternsFactory(Random rand, ISudokuSolver solver)
    {
        _rand = rand;
        _solver = solver;
        _difficultyToRemovalMapper = new Dictionary<Difficulty, IList<IRemovalPattern>>
        {
            [Difficulty.Easy] = new List<IRemovalPattern>
            {
                new CheckboardPatternRemoval(rand),
                new RandomRemovalPattern(solver, rand, Difficulty.Easy)
            },
            [Difficulty.Medium] = new List<IRemovalPattern>
            {
                new RandomRemovalPattern(solver, rand, Difficulty.Medium)
            },
            [Difficulty.Hard] = new List<IRemovalPattern>
            {
                new RandomRemovalPattern(solver, rand, Difficulty.Hard)
            }
        };
    }

    public RemovalPatternsFactory(Random rand, ISudokuSolver solver,
        Dictionary<Difficulty, IList<IRemovalPattern>> difficultyToRemovalMapper)
    {
        _rand = rand;
        _solver = solver;
        _difficultyToRemovalMapper = difficultyToRemovalMapper;
    }

    public IList<IRemovalPattern> GetRemovalPatternsFor(Difficulty difficulty)
    {
        if (!_difficultyToRemovalMapper.TryGetValue(difficulty, out var patterns))
            throw new ArgumentException($"No removal patterns defined for difficulty: {difficulty}");
        return patterns;
    }
}
