using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfShapes.Implementations.Strategies
{
    public class RandomSelectStrategy : IMoveStrategy
    {
        private readonly Random r = new Random();

        public IGameBoardCell CalculateOptimalCell(IShape shape, IGameBoardCell currentPosition, IGameBoardCell targetPosition)
        {
            var randomTable = new Dictionary<int, CellNodeMapPositions>
            {
                { 0, CellNodeMapPositions.Nord },
                { 1, CellNodeMapPositions.South },
                { 2, CellNodeMapPositions.East },
                { 3, CellNodeMapPositions.West },
                { 4, CellNodeMapPositions.NordEast },
                { 5, CellNodeMapPositions.NordWest },
                { 6, CellNodeMapPositions.SouthEast},
                { 7, CellNodeMapPositions.SouthWest },
            };


            IGameBoardCell boardCell = null;
            
            while (boardCell == null)
            {
                var i = r.Next(0, 7);
                var direction = randomTable[i];
                boardCell = currentPosition.GetMapNodes().FirstOrDefault(n => n.GetMapPosition() == direction)?.GetBoardCell();
            }

            return boardCell;
        }
    }
}
