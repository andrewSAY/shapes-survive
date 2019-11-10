using System.Collections.Generic;

namespace GameOfShapes
{
    public interface IPathAnalyzer
    {
        Dictionary<IGameBoardCell, int> AnalyzeAndGetRating(IEnumerable<IGameBoardCell> path);
    }
}
