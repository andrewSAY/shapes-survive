using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfShapes.Implementations
{
    public class GameBoardMapBuilder : IGameBoardMapBuilder
    {
        public IEnumerable<GameBoardCellMapNode> Build(int cellsCountByHorizontal, int cellsCountByVertical)
        {
            var list = new List<GameBoardCellMapNode>();

            for (var yIndex = 0; yIndex < cellsCountByHorizontal; yIndex++)
            {
                for (var hIndex = 0; hIndex < cellsCountByHorizontal; hIndex++)
                {

                }
            }
        }

        private void 
    }
}
