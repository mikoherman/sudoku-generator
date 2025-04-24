using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Geom;

namespace Sudoku_Generator.FileHandling;

public class SudokuPdfHandler : ISudokuPdfHandler
{
    //3x3 grid thickness
    private const float _outerGridThickness = 2;
    private const float _cmToPointConversionMultiplier = 28.35f;
    private readonly SolidBorder _solidBorder = new SolidBorder(_outerGridThickness);
    private readonly AreaBreak _areaBreak = new AreaBreak(AreaBreakType.NEXT_PAGE);
    public void CreatePdf(string filename,
        string mainTitle,
        IEnumerable<int[,]> sudokuBoards)
    {
        using (PdfWriter writer = new PdfWriter(filename))
        using (PdfDocument pdf = new PdfDocument(writer))
        using (Document document = new Document(pdf, PageSize.A4))
        {

            document.SetMargins(20, 20, 20, 20);
            var standardFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

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
                strToDisplay =
                    sudokuBoard[i, j] == 0 ?
                    "\u00A0\u00A0" :
                    "" + sudokuBoard[i, j];
                cell = new Cell()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                if (i == 0)
                    cell.SetBorderTop(_solidBorder);
                else if ((i + 1) % 3 == 0)
                    cell.SetBorderBottom(_solidBorder);
                if (j == 0)
                    cell.SetBorderLeft(_solidBorder);
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
