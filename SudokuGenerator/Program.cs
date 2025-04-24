using Sudoku_Generator.Factories;
using Sudoku_Generator.FileHandling;
using Sudoku_Generator.UserInteraction;
using Sudoku_Generator.Core.Validators;
using Sudoku_Generator.Core.Solvers;
using Sudoku_Generator.Core.Generators;
using Serilog;

namespace Sudoku_Generator;

public class Program
{
    private static string solvableSudokusFileName = "sudoku.pdf";
    private static string solutionsFileName = "solutions.pdf";
    private static string logFileName = "log.txt";
    static async Task Main()
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFileName)
                .CreateLogger();
            var pdfHandler = new SudokuPdfHandler();
            var random = new Random();
            var validator = new SudokuValidator();
            var solver = new SudokuSolver(validator, random);
            var userInteractor = new ConsoleUserInteractor();
            var sudokuStatus = new LoopingStatusPrinter(userInteractor);
            var sudokuGeneratorFactory = new SudokuGeneratorFactory(
                new SudokuBoardFiller(solver),
                new RemovalPatternsFactory(random, solver),
                sudokuStatus,
                random);
            var pdfFileProcessor = new PdfFileProcessor(new SudokuPdfHandler());
            var pdfStatus = new LoopingStatusPrinter(userInteractor);
            pdfFileProcessor.ProcessFinished += pdfStatus.FinishWork;
            var consoleIOProcessor = new ConsoleUserIOProcessor(
                userInteractor,
                pdfStatus,
                sudokuStatus);
            var app = new SudokuGeneratorApp(
                sudokuGeneratorFactory,
                pdfFileProcessor,
                consoleIOProcessor
                );
            await app.Run(solvableSudokusFileName, solutionsFileName);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Program has occured an unexpected exception and will be closed.");
            Log.Error($"Program has occured an unexpected error: {ex}");
            throw;
        }
        Console.WriteLine("Program is finished");
        Console.ReadKey();
    }
}