using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfShapes.Implementations;
using GameOfShapes.Implementations.MapBuilding;
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
            IPathAnalyzer pathAnalyzer,
            ISurvivalChecker survivalChecker)
        {
            var shapeCell = gameBoard.GetCellByPointer(startPosition);
            return new Shape(gameBoard, shapeType, shapeCell, moveStrategy, pathAnalyzer, survivalChecker);
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
        protected override ISurvivalChecker GetSurvivalChecker(ShapeTypes shapeType)
        {
            ISurvivalChecker survivalChecker = null;
            switch (shapeType)
            {
                case ShapeTypes.Circle:
                    survivalChecker = new SurvivalChecker(relationsCountToSurvive: 1);
                    break;

                case ShapeTypes.Square:
                    survivalChecker = new SurvivalChecker(relationsCountToSurvive: 3);
                    break;

                case ShapeTypes.Triangle:
                    survivalChecker = new SurvivalChecker(relationsCountToSurvive: 2);
                    break;
            }

            return survivalChecker;
        }

        protected override IPathAnalyzer GetPathAnalyzerForShape(ShapeTypes shapeType, ISurvivalChecker survivalChecker)
        {
            IPathAnalyzer analyzer = null;
            switch (shapeType)
            {
                case ShapeTypes.Circle:
                    analyzer = new PathAnalyzer(maxStepsOnMoveAtAll: 1, maxStepsOnMoveByDiagonal: 0, survivalChecker);
                    break;

                case ShapeTypes.Square:
                    analyzer = new PathAnalyzer(maxStepsOnMoveAtAll: 1, maxStepsOnMoveByDiagonal: 0, survivalChecker); ;
                    break;

                case ShapeTypes.Triangle:
                    analyzer = new PathAnalyzer(maxStepsOnMoveAtAll: 1, maxStepsOnMoveByDiagonal: 0, survivalChecker);
                    break;
            }

            return analyzer;
        }

    }
}
