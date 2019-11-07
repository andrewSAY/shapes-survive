using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfShapes.Implementations
{
    public class GameBoardMapBuilder : IGameBoardMapBuilder
    {
        private readonly Dictionary<Point, IGameBoardCell> _list = new Dictionary<Point, IGameBoardCell>(new PointEqualityComparer());
        private readonly int _cellsCountByHorizontal;
        private readonly int _cellsCountByVertical;

        public GameBoardMapBuilder(int cellsCountByHorizontal, int cellsCountByVertical)
        {
            _cellsCountByHorizontal = cellsCountByHorizontal;
            _cellsCountByVertical = cellsCountByHorizontal;
        }

        public IEnumerable<IGameBoardCell> Build()
        {            
            for (var xIndex = 0; xIndex < _cellsCountByHorizontal; xIndex++)
            {
                for (var yIndex = 0; yIndex < _cellsCountByVertical; yIndex++)
                {
                    var newCellPoint = new Point(xIndex, yIndex);
                    var newCell = new GameBoardCell();

                    if (!_list.ContainsKey(newCellPoint))
                    {

                    }
                }
            }
        }        
    }
}
