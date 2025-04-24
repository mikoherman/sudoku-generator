namespace Sudoku_Generator.Core.Validators;

//public class SudokuValidator
//{
//    public bool ValidateBoard(in int[,] sudokuBoard)
//    {
//        int[] rowsBitMask = new int[9];
//        int[] columnsBitMask = new int[9];
//        int[] gridBitMask = new int[9];
//        for (int i = 0; i < 9; i++)
//        {
//            for (int j = 0; j < 9; j++)
//            {
//                if (sudokuBoard[i, j] == 0)
//                    continue;
//                int value = sudokuBoard[i, j];
//                int bitPosition = 1 << (value - 1);
//                if ((rowsBitMask[i] & bitPosition) > 0)
//                    return false;
//                rowsBitMask[i] |= bitPosition;
//                if ((columnsBitMask[j] & bitPosition) > 0)
//                    return false;
//                columnsBitMask[j] |= bitPosition;
//                int subGridIndex = (i/3) * 3 + (j/3);
//                if ((gridBitMask[subGridIndex] & bitPosition) > 0)
//                    return false;
//                gridBitMask[subGridIndex] |= bitPosition;
//            }
//        }
//        return true;
//    }
//}

public class SudokuValidator : ISudokuValidator
{
    public bool Validate(int row, int col, in int[,] board)
    {
        int valueToCheck = board[row, col];
        // helper variables for 3x3 grid calculation
        int boxColStartPoint = col / 3 * 3;
        int boxRowStartPoint = row / 3 * 3;
        int boxColCurrent;
        int boxRowCurrent;
        int boardSize = board.GetLength(0);
        for (int i = 0; i < boardSize; i++)
        {
            if (i != col && board[row, i] == valueToCheck)
                return false;
            if (i != row && board[i, col] == valueToCheck)
                return false;
            // go to next row when 3x3 grid collumn end is reached
            boxColCurrent = boxColStartPoint + i % 3;
            boxRowCurrent = boxRowStartPoint + i / 3;
            if (!(boxColCurrent == col && boxRowCurrent == row) &&
                board[boxRowCurrent, boxColCurrent] == valueToCheck)
                return false;
        }
        return true;
    }
}