using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOfShapes.Implementations.MapBuilding
{
    public class GameBoardMapBuilder : IGameBoardMapBuilder
    {
        private readonly Dictionary<Point, IGameBoardCell> _map = new Dictionary<Point, IGameBoardCell>(new PointEqualityComparer());
        private readonly int _cellsCountByHorizontal;
        private readonly int _cellsCountByVertical;

        public GameBoardMapBuilder(int cellsCountByHorizontal, int cellsCountByVertical)
        {
            _cellsCountByHorizontal = cellsCountByHorizontal;
            _cellsCountByVertical = cellsCountByVertical;
        }

        public IEnumerable<IGameBoardCell> Build()
        {            
            for (var xPosition = 0; xPosition < _cellsCountByHorizontal; xPosition++)
            {
                for (var yPosition = 0; yPosition < _cellsCountByVertical; yPosition++)
                {
                    var newCell = CreateCellAndAddItToMap(xPosition, yPosition);
                    var neighborDetector = new NeighborCellDetector(newCell, _map.ToDictionary(kp  => kp.Key, kp => kp.Value));
                    neighborDetector.DetectAndSetNeighbors();
                }
            }

            return _map.Values.ToList();
        } 
        
        private IGameBoardCell CreateCellAndAddItToMap(int xPosition, int yPosition)
        {
            var newCellPoint = new Point(xPosition, yPosition);
            var newCell = new GameBoardCell(newCellPoint);

            if (!_map.ContainsKey(newCellPoint))
            {
                _map.Add(newCellPoint, newCell);
            }

            return newCell;
        }
    }
}
