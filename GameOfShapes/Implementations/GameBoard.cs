using System;
using System.Collections.Generic;
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
