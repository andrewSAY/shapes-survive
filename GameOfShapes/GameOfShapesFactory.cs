using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfShapes.Implementations;
using GameOfShapes.Implementations.MapBuilding;

namespace GameOfShapes
{
    public class GameOfShapesFactory : GameFactoryBase
    {
        public GameOfShapesFactory(Dictionary<Point, ShapeTypes> shapes, int boardCellsByHorizontal, int boardCellByVertical)
            : base(shapes, boardCellsByHorizontal, boardCellByVertical)
        {
        }

        protected override IGameBoard BuildAndGetGameBoard(IGameBoardMapBuilder gameBoardMapBuilder)
        {
            var cells = gameBoardMapBuilder.Build();
            return new GameBoard(cells);
        }

        protected override IGameBoardMapBuilder BuildAndGetGameBoardMapBuilder()
        {
            return new GameBoardMapBuilder(_boardCellsByHorizontal, _boardCellByVertical);
        }

        protected override ISession BuildAndGetSession(IEnumerable<IShape> shapes, IGameBoard gameBoard)
        {
            return new Session(shapes, gameBoard);
        }

        protected override IShape BuildAndGetShape(ShapeTypes shapeType, Point startPosition, IGameBoard gameBoard, IMoveStrategy moveStrategy)
        {
            var shapeCell = gameBoard.GetCellByPointer(startPosition);
            return new Shape(gameBoard, shapeType, shapeCell, moveStrategy);
        }
    }
}
