namespace Sudoku_Generator.Core.Models;

public record Sudoku(int[,] SolvableBoard, int[,] Solution);
