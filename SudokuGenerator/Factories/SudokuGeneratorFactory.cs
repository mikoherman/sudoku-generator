using SudokuGenerator.Models;
using SudokuGenerator.RemovalPatterns;
using Generators = SudokuGenerator.Generators;
using SudokuGenerator.Generators;
using Sudoku_Generator.UserInteraction;

namespace Sudoku_Generator.Factories;

public class SudokuGeneratorFactory : ISudokuGeneratorFactory
{
    private readonly ISudokuBoardFiller _boardFiller;
    private readonly IRemovalPatternsFactory _removalPatternsFactory;
    private readonly ILoopingStatusPrinter _loopingStatusPrinter;
    private readonly Random _rand;

    public SudokuGeneratorFactory(ISudokuBoardFiller boardFiller, IRemovalPatternsFactory removalPatternsFactory, ILoopingStatusPrinter loopingStatusPrinter, Random rand)
    {
        _boardFiller = boardFiller;
        _removalPatternsFactory = removalPatternsFactory;
        _loopingStatusPrinter = loopingStatusPrinter;
        _rand = rand;
    }

    public ISudokuGenerator CreateSudokuGeneratorFor(Difficulty difficulty)
    {
        IList<IRemovalPattern> removalPatterns = _removalPatternsFactory.GetRemovalPatternsFor(difficulty);
        var generator = new Generators.SudokuGenerator(_boardFiller, removalPatterns, _rand);
        generator.ProcessFinished += _loopingStatusPrinter.FinishWork;
        return generator;
    }
}
