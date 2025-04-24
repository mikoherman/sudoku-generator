using SudokuGenerator.Models;
using SudokuGenerator.UserInteraction;
using SudokuGenerator.Generators;
using Sudoku_Generator.Factories;
using Sudoku_Generator.FileHandling;

namespace SudokuGenerator;

public class SudokuGeneratorApp
{
    private readonly ISudokuGeneratorFactory _generatorFactory;
    private readonly IPdfFileProcessor _pdfFileProcessor;
    private readonly IConsoleUserIOProcessor _userIOProcessor;
    public SudokuGeneratorApp(ISudokuGeneratorFactory generatorFactory, 
        IPdfFileProcessor pdfFileProcessor, IConsoleUserIOProcessor userIOProcessor)
    {
        _generatorFactory = generatorFactory;
        _pdfFileProcessor = pdfFileProcessor;
        _userIOProcessor = userIOProcessor;
    }
    public async Task Run(string solvableBoardsFileName, string solutionsFileName)
    {
        int numberOfSudokuBoards = _userIOProcessor.PromptUserForNumber();
        _userIOProcessor.DisplayDifficulties();
        Difficulty sudokuDifficulty = _userIOProcessor.PromptUserForDifficulty();
        ISudokuGenerator sudokuGenerator = _generatorFactory.CreateSudokuGeneratorFor(sudokuDifficulty);
        _userIOProcessor.DisplaySudokuGeneratingStatus();
        IEnumerable<Sudoku> sudokuBoards = await sudokuGenerator
            .GenerateBoardsAsync(numberOfSudokuBoards);
        _userIOProcessor.DisplayPdfProcessingStatus();
        await _pdfFileProcessor
            .CreatePdfsFromSudokusAsync(solvableBoardsFileName, solutionsFileName, sudokuBoards);
    }
}