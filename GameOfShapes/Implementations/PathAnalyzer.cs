using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameOfShapes.Implementations;

namespace GameOfShapes.Implementations
{
    public class PathAnalyzer : IPathAnalyzer
    {
        protected  Dictionary<IGameBoardCell, int> _rating;
        protected readonly int _possibleStepsCountOnMove;
        protected readonly int _possibleStepsCountByDiagonalOnMove;
        private readonly ISurvivalChecker _survivalChecker;

        public PathAnalyzer(int maxStepsOnMoveAtAll, int maxStepsOnMoveByDiagonal, ISurvivalChecker survivalChecker)
        {
            _possibleStepsCountOnMove = maxStepsOnMoveAtAll;
            _possibleStepsCountByDiagonalOnMove = maxStepsOnMoveByDiagonal;
            _survivalChecker = survivalChecker ?? throw new ArgumentNullException(nameof(survivalChecker));
        }

        protected List<CellNodeMapPositions> _diagonalesPositions = new List<CellNodeMapPositions>
        {
            CellNodeMapPositions.NordEast, CellNodeMapPositions.NordWest, CellNodeMapPositions.SouthEast, CellNodeMapPositions.SouthWest
        };

        public Dictionary<IGameBoardCell, int> AnalyzeAndGetRating(IEnumerable<IGameBoardCell> path)
        {
            var takenCount = path.Count() >= _possibleStepsCountOnMove ? _possibleStepsCountOnMove : path.Count();
            var targetPathPart = path.Reverse().Skip(1).Take(takenCount);
            var startCell = path.Last();

            _rating = new Dictionary<IGameBoardCell, int>();

            var cellRatingPoints = _possibleStepsCountOnMove - takenCount;
            var shape = startCell.GetShapeOnCell();
            foreach( var cell in targetPathPart)
            {
                cellRatingPoints++;

                if (!ShapeWillSurviveOnCell(shape, cell)
                    || !TraceIsPossible(startCell, cell, targetPathPart))
                {
                    cellRatingPoints = 0;
                }
                
                _rating.Add(cell, cellRatingPoints);
            }

            return _rating.ToDictionary(i => i.Key, i => i.Value);
        }

        protected virtual bool ShapeWillSurviveOnCell(IShape shape, IGameBoardCell cell)
        {
            return _survivalChecker.ShapeWillSurvive(shape, cell);
        }

        protected virtual bool TraceIsPossible(IGameBoardCell startCell, IGameBoardCell targetCell, IEnumerable<IGameBoardCell> path)
        {
            var cellLeft = startCell;
            var previewLeftToRightDirection = CellNodeMapPositions.East;
            for (var index = 0; index < path.Count(); index++)
            {
                var cellRight = path.ElementAt(index);
                var stepNumber = index + 1;
                if( stepNumber > _possibleStepsCountOnMove)
                {
                    return false;
                }

                var leftToRightDirection = cellLeft.GetMapNodes().First(n => n.GetBoardCell() == cellRight).GetMapPosition();
                var leftToRightDirectionIsDiagonal = _diagonalesPositions.Contains(leftToRightDirection);
                previewLeftToRightDirection =  index == 0 ? leftToRightDirection 
                    : previewLeftToRightDirection;
                if (leftToRightDirectionIsDiagonal 
                    && leftToRightDirection == previewLeftToRightDirection
                    && stepNumber > _possibleStepsCountOnMove)
                {
                    return false;
                }

                var previewLeftToRightDirectionIsDiagonal = _diagonalesPositions.Contains(previewLeftToRightDirection);
                if ((leftToRightDirectionIsDiagonal && !previewLeftToRightDirectionIsDiagonal) 
                    || (!leftToRightDirectionIsDiagonal && previewLeftToRightDirectionIsDiagonal))
                {
                    return false;
                }
                
                if (cellRight == targetCell)
                {
                    break;
                }

                previewLeftToRightDirection = leftToRightDirection;
                cellLeft = cellRight;
            }

            return true;
        }
    }
}
