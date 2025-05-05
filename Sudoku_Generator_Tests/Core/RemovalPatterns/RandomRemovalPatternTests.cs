using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Core.Solvers;

namespace Sudoku_Generator_Tests.Core.RemovalPatterns;

[TestFixture]
public class RandomRemovalPatternTests
{
    private Mock<ISudokuSolver> _mockSudokuSolver;

    [SetUp]
    public void SetUp()
    {
        _mockSudokuSolver = new Mock<ISudokuSolver>();
    }
    [TestCase(Difficulty.Easy, 40)]
    [TestCase(Difficulty.Medium, 35)]
    [TestCase(Difficulty.Hard, 30)]
    public void ConvertBoardToSudoku_WithTestedDifficulty_ShallRemoveCorrectNumberOfCells(Difficulty testedDifficulty, int cluesLeft)
    {
        var solvedBoard = new int[,]
        {
            {5, 3, 4, 6, 7, 8, 9, 1, 2},
            {6, 7, 2, 1, 9, 5, 3, 4, 8},
            {1, 9, 8, 3, 4, 2, 5, 6, 7},
            {8, 5, 9, 7, 6, 1, 4, 2, 3},
            {4, 2, 6, 8, 5, 3, 7, 9, 1},
            {7, 1, 3, 9, 2, 4, 8, 5, 6},
            {9, 6, 1, 5, 3, 7, 2, 8, 4},
            {2, 8, 7, 4, 1, 9, 6, 3, 5},
            {3, 4, 5, 2, 8, 6, 1, 7, 9}
        };
        _mockSudokuSolver
            .Setup(solver => solver.Solve(It.IsAny<int[,]>()))
            .Returns(true);

        var cut = new RandomRemovalPattern(
            _mockSudokuSolver.Object,
            new Random(),
            testedDifficulty);

        cut.ConvertBoardToSudoku(solvedBoard);

        ClassicAssert
            .AreEqual(CountNonEmptyCellsInABoard(solvedBoard), cluesLeft);
    }

    private int CountNonEmptyCellsInABoard(int[,] board) =>
        board.Cast<int>().Where(x => x > 0).Count();

    [TestCase(Difficulty.Easy)]
    [TestCase(Difficulty.Medium)]
    [TestCase(Difficulty.Hard)]
    public void ConvertBoardToSudoku_WithAnyDifficulty_ShallThrowArgumentException_IfProvidedBoardIsInvalid(Difficulty testedDifficulty)
    {
        var invalidSudoku = new int[,]
        {
            {5, 3, 4, 6, 7, 8, 9, 1, 2},
            {6, 7, 2, 1, 9, 5, 3, 4, 8},
            {1, 9, 8, 3, 4, 2, 5, 6, 7},
            {8, 5, 9, 7, 6, 1, 4, 2, 3},
            {4, 2, 6, 8, 5, 3, 7, 9, 1},
            {7, 1, 3, 9, 2, 4, 8, 5, 6},
            {9, 6, 1, 5, 3, 7, 2, 8, 4},
            {2, 8, 7, 4, 1, 9, 6, 3, 5},
            {3, 4, 5, 2, 8, 6, 1, 7, 5}
        };
        _mockSudokuSolver
            .Setup(solver => solver.Solve(It.IsAny<int[,]>()))
            .Returns(false);
        var cut = new RandomRemovalPattern(
            _mockSudokuSolver.Object,
            new Random(),
            testedDifficulty);

        Assert
            .Throws<ArgumentException>(
            () => cut.ConvertBoardToSudoku(invalidSudoku));
    }
}
