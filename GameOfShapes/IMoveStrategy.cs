using System.Collections.Generic;

namespace GameOfShapes
{
    public interface IMoveStrategy
    {
        IEnumerable<IGameBoardCell> CalculateOptimalCellTrace(IGameBoardCell currentPosition, IGameBoardCell targetPosition, IEnumerable<IGameBoardCell> impassableCells);
    }
}
