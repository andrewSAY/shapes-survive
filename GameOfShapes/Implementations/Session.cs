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

        public Session(IEnumerable<IShape> shapes, IGameBoard gameBoard)
        {
            _shapesToPlay = shapes.ToList() ?? throw new ArgumentException(nameof(shapes));
            _gameBoard = gameBoard ?? throw new ArgumentException(nameof(gameBoard));
        }

        public void PlayRound()
        {
            for (var index = 0; index < _shapesToPlay.Count; index++)
            {
                var shape = _shapesToPlay[index];

                var nextCell = shape.NextMove();

                if(shape.IsAlive())
                {
                    var startCell = _gameBoard.GetShapeCell(shape);
                    _gameBoard.MoveShapeFromCellToCell(startCell, nextCell);
                }
                else
                {
                    _shapesToPlay.RemoveAt(index);
                }

                FireEvent();
            }
        }

        private void FireEvent()
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