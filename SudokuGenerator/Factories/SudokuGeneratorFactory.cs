using Sudoku_Generator.UserInteraction;
using Generators = Sudoku_Generator.Core.Generators;
using Sudoku_Generator.Core.RemovalPatterns;
using Sudoku_Generator.Core.Models;
using Sudoku_Generator.Core.Generators;

namespace Sudoku_Generator.Factories;

/// <summary>
/// Provides functionality to create instances of <see cref="ISudokuGenerator"/>
/// configured for specific difficulty levels.
/// </summary>
public class SudokuGeneratorFactory : ISudokuGeneratorFactory
{
    private readonly ISudokuBoardFiller _boardFiller;
    private readonly IRemovalPatternsFactory _removalPatternsFactory;
    private readonly ILoopingStatusPrinter _loopingStatusPrinter;
    private readonly Random _rand;
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuGeneratorFactory"/> class.
    /// </summary>
    /// <param name="boardFiller">An instance of <see cref="ISudokuBoardFiller"/> to generate valid Sudoku grids.</param>
    /// <param name="removalPatternsFactory">A factory to retrieve removal patterns for different difficulty levels.</param>
    /// <param name="loopingStatusPrinter">An instance of <see cref="ILoopingStatusPrinter"/> to display status updates.</param>
    /// <param name="rand">A random number generator used for selecting removal patterns.</param>
    public SudokuGeneratorFactory(ISudokuBoardFiller boardFiller, IRemovalPatternsFactory removalPatternsFactory, ILoopingStatusPrinter loopingStatusPrinter, Random rand)
    {
        _boardFiller = boardFiller;
        _removalPatternsFactory = removalPatternsFactory;
        _loopingStatusPrinter = loopingStatusPrinter;
        _rand = rand;
    }
    /// <summary>
    /// Creates an instance of <see cref="ISudokuGenerator"/> configured for the specified difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level for which to create the Sudoku generator.</param>
    /// <returns>
    /// An instance of <see cref="ISudokuGenerator"/> configured with the appropriate removal patterns.
    /// </returns>
    public ISudokuGenerator CreateSudokuGeneratorFor(Difficulty difficulty)
    {
        IList<IRemovalPattern> removalPatterns = _removalPatternsFactory.GetRemovalPatternsFor(difficulty);
        var generator = new Generators.SudokuGenerator(_boardFiller, removalPatterns, _rand);
        generator.ProcessFinished += _loopingStatusPrinter.FinishWork;
        return generator;
    }
}
