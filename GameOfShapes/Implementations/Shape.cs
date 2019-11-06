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
        private IGameBoardCell _currentPosition;


        public Shape(IGameBoard gameBoard, ShapeTypes shapeType, IGameBoardCell startPosition)
        {
            _gameBoard = gameBoard;
            _shapeType = shapeType;
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
            var r = new Random();
        }
    }
}
