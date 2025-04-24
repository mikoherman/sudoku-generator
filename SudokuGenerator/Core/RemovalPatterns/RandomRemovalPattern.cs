using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator.Core.RemovalPatterns;

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
    public RandomRemovalPattern(ISudokuSolver solver, Random rand, Difficulty difficulty)
    {
        _solver = solver;
        _rand = rand;
        _difficulty = difficulty;
    }

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
