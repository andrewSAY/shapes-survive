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
        protected readonly IPathAnalyzer _pathAnalyzer;
        protected readonly ISurvivalChecker _survivalChecker;
        protected readonly Dictionary<ShapeTypes, int> _maxMoveRadiuses = new Dictionary<ShapeTypes, int>
        {
            { ShapeTypes.Circle, 1}, {ShapeTypes.Triangle, 2}, {ShapeTypes.Square, 3}
        };
        protected IGameBoardCell _currentPosition => _gameBoard.GetShapeCell(this);
        protected IShape _connectedShape;
        protected IEnumerable<IGameBoardCell> _optimalTrace;
        List<IGameBoardCell> _impassibleCells;


        public Shape(IGameBoard gameBoard
            ,ShapeTypes shapeType
            ,IGameBoardCell startPosition
            ,IMoveStrategy strategy
            ,IPathAnalyzer pathAnalyzer
            ,ISurvivalChecker survivalChecker)
        {
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            _shapeType = shapeType;
            if (startPosition == null)
            {
                throw new ArgumentNullException(nameof(startPosition));
            }
            _moveStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _pathAnalyzer = pathAnalyzer ?? throw new ArgumentNullException(nameof(pathAnalyzer));
            _survivalChecker = survivalChecker ?? throw new ArgumentNullException(nameof(survivalChecker));

            startPosition.TrySetShapeOnCell(this);
        }

        public bool CanConnectWith(IShape shape)
        {
            if (_connectedShape == null)
            {
                return true;
            }

            return _connectedShape == shape; 
        }

        public bool ConnectWith(IShape shape)
        {
            if(!CanConnectWith(shape))
            {
                return false;
            }
            _connectedShape = shape;

            return true;
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
                if (!TryToSetOptimalTrace(_gameBoard.GetCellToWin()))
                {
                    if (!_survivalChecker.ShapeWillSurvive(this, cellToMove))
                    {
                        cellToMove = FindCellFitToSurvive();
                    }
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
        
        protected bool TryToSetOptimalTrace(IGameBoardCell targetCell)
        {
            try
            {
                _optimalTrace = _moveStrategy.CalculateOptimalCellTrace(_currentPosition, targetCell, _impassibleCells);
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

        protected IGameBoardCell FindCellFitToSurvive()
        {
            var celToMove = _currentPosition;
            foreach (var node in celToMove.GetMapNodes())
            {
                var targetCell = node.GetBoardCell();
                if (!TryToSetOptimalTrace(targetCell))
                {
                    continue;
                }

                celToMove = targetCell;
                break;
            }

            return celToMove;
        }
    }
}
