using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sudoku_Generator.Core.Solvers;
using Sudoku_Generator.Core.Validators;

namespace Sudoku_Generator_Tests.Core.Solvers;

[TestFixture]
public class SudokuSolverTests
{
    private Mock<ISudokuValidator> _mockSudokuValidator;
    private SudokuSolver _cut;

    [SetUp]
    public void SetUp()
    {
        _mockSudokuValidator = new Mock<ISudokuValidator>();
        _cut = new SudokuSolver(_mockSudokuValidator.Object, new Random());
    }
    [Test]
    public void Solve_AlreadySolvedBoard_ShallReturnTrue()
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
        _mockSudokuValidator
            .Setup(v =>
            v.Validate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int[,]>()))
            .Returns(true);

        var result = _cut.Solve(solvedBoard);
        ClassicAssert.IsTrue(result);
    }
    [Test]
    public void Solve_AlreadySolvedBoard_ShallNotChangeTheBoard()
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
        var solvedBoardCopy = (int[,])solvedBoard.Clone();
        _mockSudokuValidator
            .Setup(v =>
            v.Validate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int[,]>()))
            .Returns(true);

        _cut.Solve(solvedBoard);

        ClassicAssert.AreEqual(solvedBoardCopy, solvedBoard);
    }
    [Test]
    public void Solve_UnsolvableBoard_ShallReturnFalse()
    {
        var unsolvableBoard = new int[,]
        {
            {5, 1, 6, 8, 4, 9, 7, 3, 2},
            {3, 0, 7, 6, 0, 5, 0, 0, 0},
            {8, 0, 9, 7, 0, 0, 0, 6, 5},
            {1, 3, 5, 0, 6, 0, 9, 0, 0},
            {4, 7, 2, 5, 9, 1, 0, 0, 6},
            {9, 6, 8, 3, 7, 0, 0, 5, 0},
            {2, 5, 3, 1, 8, 6, 0, 0, 0},
            {6, 8, 4, 2, 0, 7, 5, 0, 0},
            {7, 9, 1, 0, 5, 0, 6, 0, 0}
        };
        _mockSudokuValidator
            .Setup(v =>
            v.Validate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int[,]>()))
            .Returns(false);

        var result = _cut.Solve(unsolvableBoard);
        ClassicAssert.AreEqual(false, result);
    }
}
