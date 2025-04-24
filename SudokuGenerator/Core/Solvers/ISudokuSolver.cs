namespace Sudoku_Generator.Core.Solvers;

public interface ISudokuSolver
{
    bool Solve(int[,] board);
}