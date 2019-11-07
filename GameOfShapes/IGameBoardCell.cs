using System.Collections.Generic;
using System.Drawing;

namespace GameOfShapes
{
    public interface IGameBoardCell
    {
        IEnumerable<IGameBoardCellMapNode> GetMapNodes();

        void SetMapNode(IGameBoardCellMapNode mapNode);

        Point GetPosition();

        bool CellHasShape();

        IShape GetShapeOnCell();

        bool TrySetShapeOnCell(IShape shape);

        void MoveShapeOut();        
    }
}
