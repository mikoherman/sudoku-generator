using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Geom;

namespace Sudoku_Generator.FileHandling;

/// <summary>
/// Handles the creation of PDF files containing Sudoku puzzles and their solutions.
/// </summary>
public class SudokuPdfHandler : ISudokuPdfHandler
{
    //3x3 grid thickness
    private const float _outerGridThickness = 2;
    private const float _cmToPointConversionMultiplier = 28.35f;
    private readonly SolidBorder _solidBorder = new SolidBorder(_outerGridThickness);
    private readonly AreaBreak _areaBreak = new AreaBreak(AreaBreakType.NEXT_PAGE);

    /// <summary>
    /// Creates a PDF file containing Sudoku puzzles and their solutions.
    /// </summary>
    /// <param name="filename">The name of the PDF file to create.</param>
    /// <param name="mainTitle">The main title to display at the top of the PDF.</param>
    /// <param name="sudokuBoards">A collection of Sudoku boards to include in the PDF.</param>
    public void CreatePdf(string filename,
        string mainTitle,
        IEnumerable<int[,]> sudokuBoards)
    {
        using (PdfWriter writer = new PdfWriter(filename))
        using (PdfDocument pdf = new PdfDocument(writer))
        using (Document document = new Document(pdf, PageSize.A4))
        {
            // standard document margins
            document.SetMargins(20, 20, 20, 20);
            var standardFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            // create main title to the document
            // shown only at the first page
            var paragraph = new Paragraph(mainTitle)
                .SetFont(boldFont)
                .SetFontSize(30)
                .SetTextAlignment(TextAlignment.CENTER);

            document.Add(paragraph);

            int counter = 1;
            var tableSideLengthInUnits =
                UnitValue.CreatePointValue(10.5f * _cmToPointConversionMultiplier);
            foreach (var sudokuBoard in sudokuBoards)
            {
                var sudokuTableTitle =
                    new Paragraph($"Number {counter}")
                    .SetFont(boldFont)
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    // set margin bottom to 10 to add spacing between 
                    // title and table
                    .SetMarginBottom(10);
                document.Add(sudokuTableTitle);

                var table =
                    CreateTable(standardFont,
                        sudokuBoard,
                        tableSideLengthInUnits,
                        tableSideLengthInUnits
                        );
                document.Add(table);
                if (counter % 2 == 0 && sudokuBoard != sudokuBoards.Last())
                    document.Add(_areaBreak);
                counter++;
            }

            document.Close();
        }
    }
    /// <summary>
    /// Creates a table representation of a Sudoku board for inclusion in the PDF.
    /// </summary>
    /// <param name="font">The font to use for the table text.</param>
    /// <param name="sudokuBoard">The Sudoku board represented as a 9x9 2D array.</param>
    /// <param name="width">The width of the table.</param>
    /// <param name="height">The height of the table.</param>
    /// <returns>A <see cref="Table"/> object representing the Sudoku board.</returns>
    private Table CreateTable(PdfFont font,
        int[,] sudokuBoard,
        UnitValue width,
        UnitValue height)
    {
        var table = new Table(9)
            .SetWidth(width)
            .SetHeight(height)
            .SetHorizontalAlignment(HorizontalAlignment.CENTER)
            .SetMarginBottom(30);

        string strToDisplay;
        Cell cell;
        for (int i = 0; i < sudokuBoard.GetLength(0); i++)
        {
            for (int j = 0; j < sudokuBoard.GetLength(1); j++)
            {
                // sets the string to display to either a sudoku cell number
                // or a double space to keep layout of the table intact
                strToDisplay =
                    sudokuBoard[i, j] == 0 ?
                    "\u00A0\u00A0" :
                    "" + sudokuBoard[i, j];
                cell = new Cell()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                // if top row add thick border to the top
                if (i == 0)
                    cell.SetBorderTop(_solidBorder);
                // if row number is 3, 6, or 9 add thick bottom border
                else if ((i + 1) % 3 == 0)
                    cell.SetBorderBottom(_solidBorder);
                // if first collumn set thick border left
                if (j == 0)
                    cell.SetBorderLeft(_solidBorder);
                // if column number is 3,6 or 9 add thick right border
                else if ((j + 1) % 3 == 0)
                    cell.SetBorderRight(_solidBorder);

                table.AddCell(cell
                        .Add(new Paragraph(strToDisplay)
                            .SetFont(font)
                            .SetFontSize(16)));
            }
        }
        return table;
    }
}