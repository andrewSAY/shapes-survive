using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GameOfShapes.Implementations
{
    public class Session : ISession
    {
        public event Action<Dictionary<Point, ShapeTypes>> ShapeMovedEvent;
        public event Action<IShape> SomeShapeWonEvent;
        public event Action NoShapeOnBaordLeftEvent;

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

                if(shape.IsAlive())
                {
                    _gameBoard.MoveShapeFromCellToCell(shapeCell, nextCell);
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
                    shapeCell.MoveShapeOut();
                    _shapesToPlay.Remove(shape);
                }

                FireMoveEvent();
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

            ShapeMovedEvent(eventData);
        }
    }
}