using System.Collections.Generic;

namespace GameOfShapes
{
    public interface IGameBoardMapBuilder
    {
        IEnumerable<GameBoardCellMapNode> Build(int cellsCountByHorizontal, int cellsCountByVertical); 
    }
}
