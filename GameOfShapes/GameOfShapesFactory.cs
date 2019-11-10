using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfShapes.Implementations;
using GameOfShapes.Implementations.MapBuilding;
using GameOfShapes.Implementations.PathAnalizers;
using GameOfShapes.Implementations.Strategies;

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

        protected override IShape BuildAndGetShape(ShapeTypes shapeType,
            Point startPosition,
            IGameBoard gameBoard,
            IMoveStrategy moveStrategy,
            PathAnalyzerBase pathAnalyzer)
        {
            var shapeCell = gameBoard.GetCellByPointer(startPosition);
            return new Shape(gameBoard, shapeType, shapeCell, moveStrategy, pathAnalyzer);
        }

        protected override IMoveStrategy GetMoveStrategyForShape(ShapeTypes shapeType)
        {
            IMoveStrategy strategy = null;
            switch (shapeType)
            {
                case ShapeTypes.Circle:
                    strategy = new WaveTraceStrategy(useDiagonalesDirection: false);
                    break;

                case ShapeTypes.Square:
                    strategy = new WaveTraceStrategy(useDiagonalesDirection: true);
                    break;

                case ShapeTypes.Triangle:
                    strategy = new WaveTraceStrategy(useDiagonalesDirection: true);
                    break;
            }

            return strategy;
        }

        protected override PathAnalyzerBase GetPathAnalyzerForShape(ShapeTypes shapeType)
        {
            PathAnalyzerBase analyzer = null;
            switch (shapeType)
            {
                case ShapeTypes.Circle:
                    analyzer = new CirclePathAnalyzer();
                    break;

                case ShapeTypes.Square:
                    analyzer = new SquarePathAnalyzer();
                    break;

                case ShapeTypes.Triangle:
                    analyzer = new TrianglePathAnazyzer();
                    break;
            }

            return analyzer;
        }
    }
}
