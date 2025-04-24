using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator.Factories;

/// <summary>
/// Provides functionality to create and manage removal patterns for Sudoku puzzles
/// based on the specified difficulty level.
/// </summary>
public class RemovalPatternsFactory : IRemovalPatternsFactory
{
    private readonly Random _rand;
    private readonly ISudokuSolver _solver;
    private readonly Dictionary<Difficulty, IList<IRemovalPattern>> _difficultyToRemovalMapper;
    /// <summary>
    /// Initializes a new instance of the <see cref="RemovalPatternsFactory"/> class
    /// with predefined removal patterns for each difficulty level.
    /// </summary>
    /// <param name="rand">A random number generator used to select removal patterns.</param>
    /// <param name="solver">An instance of <see cref="ISudokuSolver"/> used to validate board solvability.</param>
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
    /// <summary>
    /// Initializes a new instance of the <see cref="RemovalPatternsFactory"/> class
    /// with a custom mapping of difficulties to removal patterns.
    /// </summary>
    /// <param name="rand">A random number generator used to select removal patterns.</param>
    /// <param name="solver">An instance of <see cref="ISudokuSolver"/> used to validate board solvability.</param>
    /// <param name="difficultyToRemovalMapper">
    /// A dictionary mapping <see cref="Difficulty"/> levels to lists of <see cref="IRemovalPattern"/> objects.
    /// </param>
    public RemovalPatternsFactory(Random rand, ISudokuSolver solver,
        Dictionary<Difficulty, IList<IRemovalPattern>> difficultyToRemovalMapper)
    {
        _rand = rand;
        _solver = solver;
        _difficultyToRemovalMapper = difficultyToRemovalMapper;
    }
    /// <summary>
    /// Retrieves the list of removal patterns associated with the specified difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level for which to retrieve removal patterns.</param>
    /// <returns>
    /// A list of <see cref="IRemovalPattern"/> objects associated with the specified difficulty level.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if no removal patterns are defined for the specified difficulty level.
    /// </exception>
    public IList<IRemovalPattern> GetRemovalPatternsFor(Difficulty difficulty)
    {
        if (!_difficultyToRemovalMapper.TryGetValue(difficulty, out var patterns))
            throw new ArgumentException($"No removal patterns defined for difficulty: {difficulty}");
        return patterns;
    }
}
