using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfShapes.Implementations
{
    public class Shape : IShape
    {
        private readonly IGameBoard _gameBoard;
        private readonly ShapeTypes _shapeType;
        private readonly IMoveStrategy _moveStrategy;
        private IGameBoardCell _currentPosition => _gameBoard.GetShapeCell(this);


        public Shape(IGameBoard gameBoard, ShapeTypes shapeType, IGameBoardCell startPosition, IMoveStrategy strategy)
        {
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            _shapeType = shapeType;
            if (startPosition == null)
            {
                throw new ArgumentNullException(nameof(startPosition));
            }
            _moveStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

            startPosition.TrySetShapeOnCell(this);
        }

        public bool CanConnectWith(IShape shape)
        {
            throw new NotImplementedException();
        }

        public bool ConnectWith(IShape shape)
        {
            throw new NotImplementedException();
        }

        public Point GetPosition()
        {
            return _currentPosition.GetPosition();
        }

        public ShapeTypes GetShapeType()
        {
            return _shapeType;
        }

        public bool IsAlive()
        {
            return true;
        }

        public IGameBoardCell NextMove()
        {
            return _moveStrategy.CalculateOptimalCell(this, _currentPosition, _gameBoard.GetCellToWin());
        }
    }
}
