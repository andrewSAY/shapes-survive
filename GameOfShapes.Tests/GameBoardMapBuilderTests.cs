using System;
using System.Linq;
using Xunit;
using GameOfShapes.Implementations;

namespace GameOfShapes.Tests
{
    public class GameBoardMapBuilderTests
    {
        [Theory]
        [InlineData(10, 20)]
        [InlineData(11, 17)]
        [InlineData(119, 65)]
        public void Build_Default_ReturnsExpectedCountOfNodes(int width, int height)
        {
            var builder = new GameBoardMapBuilder();

            var nodes = builder.Build(width, height);

            Assert.Equal(width * height, nodes.Count());
        }

        [Fact]       
        public void Build_Default_ReturnsExpectedGraph()
        {
            var builder = new GameBoardMapBuilder();

            var nodes = builder.Build(cellsCountByHorizontal: 4, cellsCountByVertical: 4);

            
        }

        [Fact]
        public void Build_Default_ReturnsNoRepeatCells()
        {
            var builder = new GameBoardMapBuilder();

            var nodes = builder.Build(cellsCountByHorizontal: 4, cellsCountByVertical: 4);


        }
    }
}
