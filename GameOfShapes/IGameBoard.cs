using System.Drawing;

namespace GameOfShapes
{
    public interface IGameBoard
    {
        IGameBoardCell GetShapeCell(IShape shape);

        IGameBoardCell GetCellByPointer(Point cellPointer);

        IGameBoardCell GetCellToWin();

        void MoveShapeFromCellToCell(IGameBoardCell fromCell, IGameBoardCell toCell);
    }
}
