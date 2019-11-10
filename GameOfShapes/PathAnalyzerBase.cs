using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfShapes
{
    public abstract class PathAnalyzerBase
    {
        protected  Dictionary<IGameBoardCell, int> _rating;
        protected abstract int PossibleStepsCountOnMove { get; }
        protected abstract int PossibleStepsCountByDiagonalOnMove { get; }

        protected List<CellNodeMapPositions> _diagonalesPositions = new List<CellNodeMapPositions>
        {
            CellNodeMapPositions.NordEast, CellNodeMapPositions.NordWest, CellNodeMapPositions.SouthEast, CellNodeMapPositions.SouthWest
        };

        public Dictionary<IGameBoardCell, int> AnalyzeAndGetRating(IEnumerable<IGameBoardCell> path)
        {
            var takenCount = path.Count() >= PossibleStepsCountOnMove ? PossibleStepsCountOnMove : path.Count();
            var targetPathPart = path.Reverse().Skip(1).Take(takenCount);
            var startCell = path.Last();

            _rating = new Dictionary<IGameBoardCell, int>();

            var cellRatingPoints = PossibleStepsCountOnMove - takenCount;
            
            foreach( var cell in targetPathPart)
            {
                cellRatingPoints++;

                if(!ShapeWillSurviveOnCell(cell)
                    || !TraceIsPossible(startCell, cell, targetPathPart))
                {
                    cellRatingPoints = 0;
                }
                
                _rating.Add(cell, cellRatingPoints);
            }

            return _rating.ToDictionary(i => i.Key, i => i.Value);
        }

        protected abstract bool ShapeWillSurviveOnCell(IGameBoardCell cell);

        protected virtual bool TraceIsPossible(IGameBoardCell startCell, IGameBoardCell targetCell, IEnumerable<IGameBoardCell> path)
        {
            var cellLeft = startCell;
            var previewLeftToRightDirection = CellNodeMapPositions.East;
            for (var index = 0; index < path.Count(); index++)
            {
                var cellRight = path.ElementAt(index);
                var stepNumber = index + 1;
                if( stepNumber > PossibleStepsCountOnMove)
                {
                    return false;
                }

                var leftToRightDirection = cellLeft.GetMapNodes().First(n => n.GetBoardCell() == cellRight).GetMapPosition();
                var leftToRightDirectionIsDiagonal = _diagonalesPositions.Contains(leftToRightDirection);
                previewLeftToRightDirection =  index == 0 ? leftToRightDirection 
                    : previewLeftToRightDirection;
                if (leftToRightDirectionIsDiagonal 
                    && leftToRightDirection == previewLeftToRightDirection
                    && stepNumber > PossibleStepsCountByDiagonalOnMove)
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
