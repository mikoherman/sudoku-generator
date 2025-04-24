namespace SudokuGenerator.Models;

public record Sudoku(int[,] SolvableBoard, int[,] Solution);
