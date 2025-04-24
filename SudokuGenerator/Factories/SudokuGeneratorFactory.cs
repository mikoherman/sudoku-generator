using Sudoku_Generator.UserInteraction;
using Generators = Sudoku_Generator.Core.Generators;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.Generators;

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
