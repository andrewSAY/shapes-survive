using System.Drawing;

namespace GameOfShapes
{
    public interface IGameBoardCell
    {

        Point GetPosition();

        bool CellHasShape();

        IShape GetShapeOnCell();

        bool TrySetShapeOnCell(IShape shape);

        void MoveShapeOut();        
    }
}
