using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sudoku_Generator.Core.Validators;

namespace Sudoku_Generator_Tests.Core.Validators;

[TestFixture]
public class SudokuValidatorTests
{
    private SudokuValidator _cut;
    [SetUp]
    public void SetUp()
    {
        _cut = new SudokuValidator();
    }
    [Test]
    public void Validate_ShallReturnFalse_IfThereIsADuplicateInTheCurrentRow()
    {
        int insertedValueRow = 4;
        int insertedValueCol = 4;
        var boardToValidate = new int[,]
        {
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 5,0,0,0, 5 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 }
        };

        var result = _cut.Validate(insertedValueRow, insertedValueCol, boardToValidate);

        ClassicAssert.AreEqual(false, result);
    }
    [Test]
    public void Validate_ShallReturnFalse_IfThereIsADuplicateInTheCurrentColumn()
    {
        int insertedValueRow = 4;
        int insertedValueCol = 4;
        var boardToValidate = new int[,]
        {
            { 0, 0, 0, 0, 5,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 5,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 }
        };

        var result = _cut.Validate(insertedValueRow, insertedValueCol, boardToValidate);

        ClassicAssert.AreEqual(false, result);
    }
    [Test]
    public void Validate_ShallReturnFalse_IfThereIsADuplicateInTheCurrent3x3Grid()
    {
        int insertedValueRow = 4;
        int insertedValueCol = 4;
        var boardToValidate = new int[,]
        {
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 5, 1,2,0,0, 0 },
            { 0, 0, 0, 4, 5,3,0,0, 0 },
            { 0, 0, 0, 6, 8,9,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 },
            { 0, 0, 0, 0, 0,0,0,0, 0 }
        };

        var result = _cut.Validate(insertedValueRow, insertedValueCol, boardToValidate);

        ClassicAssert.AreEqual(false, result);
    }
    [Test]
    public void Validate_ShallReturnTrue_IfThereAreNoDuplicatesInRowColumnAnd3x3Grid()
    {
        int insertedValueRow = 4;
        int insertedValueCol = 4;
        var boardToValidate = new int[,]
        {
            { 0, 0, 0, 0, 2,0,0,0, 0 },
            { 0, 0, 0, 0, 3,0,0,0, 0 },
            { 0, 0, 0, 0, 4,0,0,0, 0 },
            { 0, 0, 0, 7, 1,2,0,0, 0 },
            { 1, 2, 6, 4, 5,3,7,8, 9 },
            { 0, 0, 0, 6, 8,9,0,0, 0 },
            { 0, 0, 0, 0, 9,0,0,0, 0 },
            { 0, 0, 0, 0, 7,0,0,0, 0 },
            { 0, 0, 0, 0, 6,0,0,0, 0 }
        };

        var result = _cut.Validate(insertedValueRow, insertedValueCol, boardToValidate);

        ClassicAssert.AreEqual(true, result);
    }
}