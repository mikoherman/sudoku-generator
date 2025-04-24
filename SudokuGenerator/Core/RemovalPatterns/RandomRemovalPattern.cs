using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator.Core.RemovalPatterns;

/// <summary>
/// Implements a random removal pattern for generating Sudoku puzzles
/// by removing numbers from a fully solved board based on the specified difficulty.
/// </summary>
public class RandomRemovalPattern : IRemovalPattern
{
    private readonly ISudokuSolver _solver;
    private readonly Random _rand;
    private readonly Difficulty _difficulty;
    private readonly Dictionary<Difficulty, int> _difficultyToCluesMapper = new()
    {
        [Difficulty.Easy] = 40,
        [Difficulty.Medium] = 35,
        [Difficulty.Hard] = 30
    };
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomRemovalPattern"/> class.
    /// </summary>
    /// <param name="solver">An instance of <see cref="ISudokuSolver"/> to validate board solvability.</param>
    /// <param name="rand">A random number generator for selecting cells to remove.</param>
    /// <param name="difficulty">The difficulty level for the Sudoku puzzle.</param>
    public RandomRemovalPattern(ISudokuSolver solver, Random rand, Difficulty difficulty)
    {
        _solver = solver;
        _rand = rand;
        _difficulty = difficulty;
    }
    /// <summary>
    /// Converts a fully solved Sudoku board into a puzzle by removing numbers
    /// based on the specified difficulty level.
    /// </summary>
    /// <param name="board">The fully solved Sudoku board represented as a 9x9 2D array.</param>
    /// <returns>
    /// A <see cref="Sudoku"/> object containing the puzzle board and its solution.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the board cannot be converted into a solvable puzzle using this pattern.
    /// </exception>
    public Sudoku ConvertBoardToSudoku(int[,] board)
    {
        int numbersToRemove = 81 - _difficultyToCluesMapper[_difficulty];
        IList<int> indexes = Enumerable.Range(0, 81).OrderBy(_ => _rand.Next()).ToList();
        int[,] solution = (int[,])board.Clone();
        foreach (int idx in indexes)
        {
            if (numbersToRemove == 0) 
                break;
            int row = idx / 9;
            int col = idx % 9;
            int backup = board[row, col];
            board[row, col] = 0;

            solution = (int[,])board.Clone();
            if (_solver.Solve(solution))
            {
                numbersToRemove--;
            }
            else
            {
                board[row, col] = backup;
            }
        }
        if (numbersToRemove > 0)
        {
            throw new ArgumentException($"The board: {board} cannot be converted to solvable using this pattern: {nameof(RandomRemovalPattern)}, with this difficulty: {_difficulty}");
        }
        return new Sudoku(board, solution);
    }
}
