using System;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations
{
    public class SurvivalChecker : ISurvivalChecker
    {
        private readonly int _relationsCountToSurvive;

        public SurvivalChecker(int relationsCountToSurvive)
        {
            _relationsCountToSurvive = relationsCountToSurvive;
        }

        public bool ShapeWillSurvive(IShape shape, IGameBoardCell cell)
        {
            var foundPossibleRelationsCount = 0;

            foreach (var node in cell.GetMapNodes())
            {
                var shapeToConnect = GetShapeOnCell(node.GetBoardCell());
                if (shapeToConnect != null
                    && shapeToConnect != shape
                    && shapeToConnect.CanConnectWith(shape))
                {
                    foundPossibleRelationsCount++;
                }
            }

            return foundPossibleRelationsCount >= _relationsCountToSurvive;
        }

        protected IShape GetShapeOnCell(IGameBoardCell cell)
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
