using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations
{
    public class GameBoardCell : IGameBoardCell
    {
        private readonly Point _position;
        private IShape _shapeOnCell;

        public GameBoardCell(Point position)
        {
            _position = position;
        }        

        public bool CellHasShape()
        {
            return _shapeOnCell != null;
        }      

        public Point GetPosition()
        {
            return _position;
        }

        public IShape GetShapeOnCell()
        {
            if(_shapeOnCell == null)
            {
                throw new NoShapeOnCellExeption();
            }
            return _shapeOnCell;
        }

        public void MoveShapeOut()
        {
            if (_shapeOnCell == null)
            {
                throw new NoShapeOnCellExeption();
            }

            _shapeOnCell = null;
        }

        public bool TrySetShapeOnCell(IShape shape)
        {
            if(shape != null)
            {
                return false;
            }

            _shapeOnCell = shape;

            return true;
        }
    }
}
