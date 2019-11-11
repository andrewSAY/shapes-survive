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
        private readonly Dictionary<ShapeTypes, int> _relationsToSurvive = new Dictionary<ShapeTypes, int>
        {
            { ShapeTypes.Circle, 1}, {ShapeTypes.Triangle, 2}, {ShapeTypes.Square, 3}
        };

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
            if(fromCell == toCell)
            {
                return;
            }

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
            shapeToMove.BreakConnectionsIfCantToSave();
            ConnectShape(shapeToMove, toCell);
        }

        private void ConnectShape(IShape shape, IGameBoardCell shapeCell)
        {
            var relationsToSurvive = _relationsToSurvive[shape.GetShapeType()];

            foreach (var node in shapeCell.GetMapNodes())
            {
                if(relationsToSurvive <= 0)
                {
                    break;
                }

                var shapeToConnect = GetShapeFromCell(node.GetBoardCell());
                if (shapeToConnect != null
                    && shapeToConnect.CanConnectWith(shape)
                    && shape.CanConnectWith(shapeToConnect))
                {
                    if (!shape.ConnectWith(shapeToConnect) || !shapeToConnect.ConnectWith(shape))
                    {
                        throw new ClosedConnectionBetweenShapesImpossibleException(shape.GetPosition(), shapeToConnect.GetPosition());
                    }
                    relationsToSurvive--;
                }
            }
        }

        private IShape GetShapeFromCell(IGameBoardCell cell)
        {
            try
            {
                return cell.GetShapeOnCell();
            }
            catch (NoShapeOnCellExeption)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
