
namespace Sudoku_Generator.FileHandling;

public interface ISudokuPdfHandler
{
    void CreatePdf(string filename, string mainTitle, IEnumerable<int[,]> sudokuBoards);
}