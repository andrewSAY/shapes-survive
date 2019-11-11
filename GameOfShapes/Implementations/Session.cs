using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GameOfShapes.Implementations
{
    public class Session : ISession
    {
        public event Action<Dictionary<Point, ShapeTypes>, Dictionary<Point, Point>> ShapeMovedEvent;
        public event Action<IShape> SomeShapeWonEvent;
        public event Action NoShapeOnBoardHasLeftEvent;

        private readonly List<IShape> _shapesToPlay;
        private readonly IGameBoard _gameBoard;
        private IShape _shapeWinner;

        public Session(IEnumerable<IShape> shapes, IGameBoard gameBoard)
        {
            _shapesToPlay = shapes.ToList() ?? throw new ArgumentException(nameof(shapes));
            _gameBoard = gameBoard ?? throw new ArgumentException(nameof(gameBoard));
        }

        public void PlayRound()
        {
            if (_shapeWinner != null)
            {
                FireWinEvent();
                return;
            }

            for (var index = 0; index < _shapesToPlay.Count; index++)
            {
                var shape = _shapesToPlay[index];
                var nextCell = shape.NextMove();
                var shapeCell = _gameBoard.GetShapeCell(shape);

                _gameBoard.MoveShapeFromCellToCell(shapeCell, nextCell);
                
                if(shape.IsAlive())
                {
                    if(nextCell == _gameBoard.GetCellToWin())
                    {
                        _shapeWinner = shape;
                        FireMoveEvent();
                        FireWinEvent();
                        break;
                    }
                }
                else
                {
                    nextCell.MoveShapeOut();
                    _shapesToPlay.Remove(shape);
                }

                FireMoveEvent();
            }

            if (!_shapesToPlay.Any())
            {
                FireFailEvent();
            }
        }

        private void FireWinEvent()
        {
            if(SomeShapeWonEvent == null)
            {
                return;
            }

            SomeShapeWonEvent(_shapeWinner);
        }

        private void FireMoveEvent()
        {
            if(ShapeMovedEvent == null)
            {
                return;
            }

            var eventData = _shapesToPlay.ToDictionary(s => s.GetPosition(), s => s.GetShapeType(), new PointEqualityComparer());
            var points = new Dictionary<Point, Point>(new PointEqualityComparer());
            // I know it is O(N^2) but i'm tired :)
            foreach (var shape in _shapesToPlay)
            {
                foreach (var shapeToConnect in _shapesToPlay)
                {
                    if(shape.IsConnectedWith(shapeToConnect)
                        || shapeToConnect.IsConnectedWith(shape))
                    {
                        if (!points.ContainsKey(shape.GetPosition()))
                        {
                            points.Add(shape.GetPosition(), shapeToConnect.GetPosition());
                        }
                    }
                }
            }

            ShapeMovedEvent(eventData, points);
        }

        private void FireFailEvent()
        {
            if(NoShapeOnBoardHasLeftEvent == null)
            {
                return;
            }

            NoShapeOnBoardHasLeftEvent();
        }
    }
}