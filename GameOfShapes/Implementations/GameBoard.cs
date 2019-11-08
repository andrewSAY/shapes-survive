using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations
{
    public class GameBoard : IGameBoard
    {
        private readonly IEnumerable<IGameBoardCell> _cells;

        public GameBoard(IEnumerable<IGameBoardCell> cells)
        {
            _cells = cells.ToList();
        }

        public IGameBoardCell GetCellByPointer(Point cellPointer)
        {
            var cell = _cells.FirstOrDefault(c => c.GetPosition().X == cellPointer.X && c.GetPosition().Y == cellPointer.Y);
            
            if(cell == null)
            {
                throw new NoCellOnBoardException(cellPointer);
            }

            return cell;
        }

        public IGameBoardCell GetCellToWin()
        {
            return _cells.Last();
        }

        public IGameBoardCell GetShapeCell(IShape shape)
        {
            var cellWithShape = _cells.FirstOrDefault(c => c.CellHasShape() && ReferenceEquals(c.GetShapeOnCell(), shape));

            if (cellWithShape == null)
            {
                throw new NoCellWithShapeException();
            }

            return cellWithShape;
        }

        public void MoveShapeFromCellToCell(IGameBoardCell fromCell, IGameBoardCell toCell)
        {
            if (toCell.CellHasShape())
            {
                throw new ImpossibleMoveShapeToNoFreeCellException(fromCell, toCell);
            }

            var shapeToMove = fromCell.GetShapeOnCell();

            if (!toCell.TrySetShapeOnCell(shapeToMove))
            {
                throw new ApplicationException($"The shape wasn't moved to target cell (X:{toCell.GetPosition().X}; Y:{toCell.GetPosition().Y})");
            }

            fromCell.MoveShapeOut();
        }
    }
}
