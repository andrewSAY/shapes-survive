using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations
{
    public class GameBoardCell : IGameBoardCell
    {
        private readonly Point _position;
        private IShape _shapeOnCell;
        private Dictionary<CellNodeMapPositions, IGameBoardCellMapNode> _map = new Dictionary<CellNodeMapPositions, IGameBoardCellMapNode>();

        public GameBoardCell(Point position)
        {
            _position = position;
        }        

        public bool CellHasShape()
        {
            return _shapeOnCell != null;
        }

        public IEnumerable<IGameBoardCellMapNode> GetMapNodes()
        {
            return _map.Values.ToList();
        }

        public void SetMapNode(IGameBoardCellMapNode mapNode)
        {
            var mapNodeRelativePosition = mapNode.GetMapPosition();

            if (!_map.ContainsKey(mapNodeRelativePosition))
            {
                _map.Add(mapNodeRelativePosition, mapNode);
            }
            else
            {
                _map[mapNodeRelativePosition] = mapNode;
            }
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
