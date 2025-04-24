using Sudoku_Generator.Core.Validators;

namespace Sudoku_Generator.Core.Solvers;

public class SudokuSolver : ISudokuSolver
{
    private readonly ISudokuValidator _validator;
    private readonly Random _rand;

    public SudokuSolver(ISudokuValidator validator, Random rand)
    {
        _validator = validator;
        _rand = rand;
    }

    public bool Solve(int[,] board) =>
        Solve(0, 0, board);

    private bool Solve(int row, int col, int[,] board)
    {
        if (row == 9)
            return true;
        else if (col == 9)
            return Solve(row + 1, 0, board);
        else if (board[row, col] != 0)
            return Solve(row, col + 1, board);
        else
        {
            var possibleNumbers = Enumerable.Range(1, 9).ToList();
            while (possibleNumbers.Any())
            {
                int index = _rand.Next(possibleNumbers.Count());
                int numberToTest = possibleNumbers[index];
                possibleNumbers.RemoveAt(index);
                board[row, col] = numberToTest;
                if (_validator.Validate(row, col, board))
                {
                    if (Solve(row, col + 1, board))
                        return true;
                }
                board[row, col] = 0;
            }

            return false;
        }
    }
}


