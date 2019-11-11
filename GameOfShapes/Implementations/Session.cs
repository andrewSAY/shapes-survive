using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GameOfShapes.Implementations
{
    public class Session : ISession
    {
        public event Action<Dictionary<Point, ShapeTypes>, IEnumerable<(Point, Point)>> ShapeMovedEvent;
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
            var shapesToRemove = new List<IShape>();
            foreach (var shape in _shapesToPlay)
            {
                var nextCell = shape.NextMove();
                var shapeCell = _gameBoard.GetShapeCell(shape);

                _gameBoard.MoveShapeFromCellToCell(shapeCell, nextCell);
                
                if(shape.IsAlive())
                {
                    if(nextCell == _gameBoard.GetCellToWin())
                    {
                        _shapeWinner = shape;
                        FireMoveEvent(shapesToRemove);
                        FireWinEvent();
                        break;
                    }
                }
                else
                {
                    nextCell.MoveShapeOut();
                    shapesToRemove.Add(shape);
                }
                
                FireMoveEvent(shapesToRemove);
            }

            _shapesToPlay.RemoveAll(s => shapesToRemove.Contains(s));
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

        private void FireMoveEvent(List<IShape> shapesToRemove)
        {
            if(ShapeMovedEvent == null)
            {
                return;
            }
            var shapes = _shapesToPlay.ToList();
            shapes.RemoveAll(s => shapesToRemove.Contains(s));

            var eventData = shapes.ToDictionary(s => s.GetPosition(), s => s.GetShapeType(), new PointEqualityComparer());
            var points = new List<(Point, Point)>();
            // I know it is O(N^2) but i'm tired :)
            foreach (var shape in shapes)
            {
                foreach (var shapeToConnect in shapes)
                {
                    if(shape.IsConnectedWith(shapeToConnect)
                        || shapeToConnect.IsConnectedWith(shape))
                    {
                        points.Add((shape.GetPosition(), shapeToConnect.GetPosition()));
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