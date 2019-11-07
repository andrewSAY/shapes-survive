using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using GameOfShapes.Implementations.MapBuilding;

namespace GameOfShapes.Tests
{
    public class GameBoardMapBuilderTests
    {
        [Theory]
        [InlineData(5, 5)]
        [InlineData(10, 10)]
        [InlineData(10, 20)]
        [InlineData(11, 17)]
        [InlineData(119, 65)]
        public void Build_Default_ReturnsExpectedCountOfNodes(int width, int height)
        {
            var builder = new GameBoardMapBuilder(width, height);

            var cells = builder.Build();

            Assert.Equal(width * height, cells.Count());
        }

        [Fact]       
        public void Build_Default_ReturnsExpectedGraph()
        {
            var builder = new GameBoardMapBuilder(cellsCountByHorizontal: 3, cellsCountByVertical: 3);

            var cells = builder.Build().ToArray();

            Assert.Equal(3, cells[0].GetMapNodes().Count());
            Assert.Equal(5, cells[1].GetMapNodes().Count());
            Assert.Equal(3, cells[2].GetMapNodes().Count());
            
            Assert.Equal(5, cells[3].GetMapNodes().Count());
            Assert.Equal(8, cells[4].GetMapNodes().Count());
            Assert.Equal(5, cells[5].GetMapNodes().Count());

            Assert.Equal(3, cells[6].GetMapNodes().Count());
            Assert.Equal(5, cells[7].GetMapNodes().Count());
            Assert.Equal(3, cells[8].GetMapNodes().Count());
        }       

        [Fact]
        public void Build_Default_ReturnsNoRepeatCells()
        {
            var builder = new GameBoardMapBuilder(cellsCountByHorizontal: 3, cellsCountByVertical: 4);

            var cells = builder.Build();

            var hashSetCells = new HashSet<IGameBoardCell>();

            foreach(var cell in cells)
            {
                hashSetCells.Add(cell);
            }

            Assert.Equal(cells.Count(), hashSetCells.Count);
        }
    }
}
