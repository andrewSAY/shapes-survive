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
        protected readonly HashSet<IShape> _connectedShapes;
        protected readonly int _relationsCount;
        protected IGameBoardCell _currentPosition => _gameBoard.GetShapeCell(this);
        protected IEnumerable<IGameBoardCell> _optimalTrace;
        List<IGameBoardCell> _impassibleCells;


        public Shape(IGameBoard gameBoard
            ,ShapeTypes shapeType
            ,IGameBoardCell startPosition
            ,IMoveStrategy strategy
            ,IPathAnalyzer pathAnalyzer
            ,ISurvivalChecker survivalChecker
            ,int relationsCount)
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
            _connectedShapes = new HashSet<IShape>();
            _relationsCount = relationsCount;
            startPosition.TrySetShapeOnCell(this);
        }

        public bool CanConnectWith(IShape shape)
        {
            if (_connectedShapes.Contains(shape))
            {
                return true;
            }

            return _connectedShapes.Count() < _relationsCount; 
        }

        public bool ConnectWith(IShape shape)
        {
            if(!CanConnectWith(shape))
            {
                return false;
            }
            _connectedShapes.Add(shape);

            return true;
        }

        public bool IsConnectedWith(IShape shape)
        {
            return _connectedShapes.Contains(shape);
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
            return _survivalChecker.ShapeWillSurvive(this, _currentPosition);
        }

        public IGameBoardCell NextMove()
        {
            _impassibleCells = new List<IGameBoardCell>();
            var cellToMove = _currentPosition;
           
            while (true)
            {
                if (!TryToSetOptimalTrace(_gameBoard.GetCellToWin()))
                {
                    if (cellToMove == _currentPosition)
                    {
                        cellToMove = FindCellFitToSurvive();
                    }

                    if (cellToMove == _currentPosition)
                    {
                        cellToMove = FindAnyCellToTheLastStep();
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
        
        protected IGameBoardCell FindCellFitToSurvive()
        {
            return FindAnyCell(_survivalChecker.ShapeWillSurvive);           
        }

        protected IGameBoardCell FindAnyCellToTheLastStep()
        {
            return FindAnyCell((IShape, IGameBoardCell) => true);
        }

        protected IGameBoardCell FindAnyCell(Func<IShape, IGameBoardCell, bool> checkSurvival)
        {
            var cellToMove = _currentPosition;
            foreach (var node in cellToMove.GetMapNodes())
            {
                var targetCell = node.GetBoardCell();
                if (!TryToSetOptimalTrace(targetCell))
                {
                    continue;
                }

                if (!targetCell.CellHasShape() && checkSurvival(this, targetCell))
                {
                    cellToMove = targetCell;
                    break;
                }
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

        public void BreakConnectionsIfCantToSave()
        {
            var relationsToRemove = new List<IShape>();
            foreach(var shape in _connectedShapes)
            {
                var itselfPosition = _currentPosition.GetPosition();
                var shapePosition = shape.GetPosition();

                var xDistance = Math.Abs(itselfPosition.X - shapePosition.X);
                var yDistance = Math.Abs(itselfPosition.Y - shapePosition.Y);

                if(xDistance > 1 || yDistance > 1)
                {
                    relationsToRemove.Add(shape);
                }
            }

            _connectedShapes.RemoveWhere(s => relationsToRemove.Contains(s));
        }

        private IShape GetShapeFromCell(IGameBoardCell cell)
        {
            try
            {
                return cell.GetShapeOnCell();
            }
            catch (NoShapeOnCellExeption)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
