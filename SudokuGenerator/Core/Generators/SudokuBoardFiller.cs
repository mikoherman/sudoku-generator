using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator.Core.Generators;

public class SudokuBoardFiller : ISudokuBoardFiller
{
    private readonly ISudokuSolver _solver;

    public SudokuBoardFiller(ISudokuSolver solver)
    {
        _solver = solver;
    }

    public int[,] GenerateValidSudokuGrid()
    {
        int[,] sudokuBoard;
        do
        {
            sudokuBoard = new int[9, 9];
        } while (!_solver.Solve(sudokuBoard));
        return sudokuBoard;
    }
}


