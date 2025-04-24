namespace SudokuGenerator.Solvers;

public interface ISudokuSolver
{
    bool Solve(int[,] board);
}