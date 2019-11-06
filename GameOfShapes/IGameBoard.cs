using System.Collections.Generic;

namespace GameOfShapes
{
    public interface IGameBoard
    {
        IGameBoardCell GetShapeCell(IShape shape);

        void MoveShapeFromCellToCell(IGameBoardCell fromCell, IGameBoardCell toCell);
    }
}
