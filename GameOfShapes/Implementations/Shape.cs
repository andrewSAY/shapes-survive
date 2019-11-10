using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations
{
    public class Shape : IShape
    {
        protected readonly IGameBoard _gameBoard;
        protected readonly ShapeTypes _shapeType;
        protected readonly IMoveStrategy _moveStrategy;
        protected readonly PathAnalyzerBase _pathAnalyzer;
        protected IGameBoardCell _currentPosition => _gameBoard.GetShapeCell(this);
       
        protected IEnumerable<IGameBoardCell> _optimalTrace;
        List<IGameBoardCell> _impassibleCells;


        public Shape(IGameBoard gameBoard, ShapeTypes shapeType, IGameBoardCell startPosition, IMoveStrategy strategy, PathAnalyzerBase pathAnalyzer)
        {
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            _shapeType = shapeType;
            if (startPosition == null)
            {
                throw new ArgumentNullException(nameof(startPosition));
            }
            _moveStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _pathAnalyzer = pathAnalyzer ?? throw new ArgumentNullException(nameof(pathAnalyzer));

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
            _impassibleCells = new List<IGameBoardCell>();
            var cellToMove = _currentPosition;
           
            while (true)
            {
                if (!TryToSetOptimalTrace())
                {
                    break;
                }

                var ratingCells = _pathAnalyzer.AnalyzeAndGetRating(_optimalTrace);

                var maxPoints = ratingCells.Values.Max();
                var maxPointsCell = ratingCells.FirstOrDefault(i => i.Value == maxPoints).Key;

                if (maxPointsCell != null
                    && maxPoints > 0
                    && !maxPointsCell.CellHasShape()
                    )
                {
                    cellToMove = maxPointsCell;
                    break;
                }

                if(maxPointsCell.CellHasShape())
                {
                    _impassibleCells.Add(maxPointsCell);
                }
                var ratingZeroCells = ratingCells.Where(i => i.Value == 0).Select(i => i.Key);
                _impassibleCells.AddRange(ratingZeroCells);
            }

            return cellToMove;
        }
        
        private bool TryToSetOptimalTrace()
        {
            try
            {
                _optimalTrace = _moveStrategy.CalculateOptimalCellTrace(_currentPosition, _gameBoard.GetCellToWin(), _impassibleCells);
            }
            catch (NoPathException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
