namespace Sudoku_Generator.Core.Models;

/// <summary>
/// Represents a Sudoku puzzle, including both the solvable board and its solution.
/// </summary>
/// <param name="SolvableBoard">
/// The Sudoku board presented to the user for solving. 
/// This is a 9x9 grid where some cells are pre-filled, and others are empty.
/// </param>
/// <param name="Solution">
/// The complete solution to the Sudoku puzzle. 
/// This is a 9x9 grid where all cells are filled with valid numbers according to Sudoku rules.
/// </param>
public record Sudoku(int[,] SolvableBoard, int[,] Solution);
