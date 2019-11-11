using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfShapes.Implementations.Strategies;

namespace GameOfShapes
{
    public abstract class GameFactoryBase
    {
        protected Dictionary<Point, ShapeTypes> _shapesDescriptions;
        protected int _boardCellsByHorizontal;
        protected int _boardCellByVertical;
        protected Dictionary<ShapeTypes, int> _relationsCountSurvive = new Dictionary<ShapeTypes, int>
        {
            { ShapeTypes.Circle, 1 }, { ShapeTypes.Triangle, 2 }, { ShapeTypes.Square, 3 }
        };

        public GameFactoryBase(Dictionary<Point, ShapeTypes> shapes, int boardCellsByHorizontal, int boardCellByVertical)
        {
            _shapesDescriptions = shapes ?? throw new ArgumentNullException(nameof(shapes));
            _boardCellsByHorizontal = boardCellsByHorizontal;
            _boardCellByVertical = boardCellByVertical;
        }

        public ISession BuildGameAndGetSession()
        {
            var mapBuilder = BuildAndGetGameBoardMapBuilder();
            var board = BuildAndGetGameBoard(mapBuilder);
            var shapes = GetShapesForDescriptions(board);

            return BuildAndGetSession(shapes, board);
        }

        protected virtual IEnumerable<IShape> GetShapesForDescriptions(IGameBoard gameBoard)
        {
            foreach (var description in _shapesDescriptions)
            {
                var moveStrategy = GetMoveStrategyForShape(description.Value);
                var survivalChecker = GetSurvivalChecker(description.Value);
                var analyzer = GetPathAnalyzerForShape(description.Value, survivalChecker);
                yield return BuildAndGetShape(description.Value, description.Key, gameBoard, moveStrategy, analyzer, survivalChecker);
            }
        }

        protected abstract ISession BuildAndGetSession(IEnumerable<IShape> shapes, IGameBoard gameBoard);

        protected abstract IGameBoardMapBuilder BuildAndGetGameBoardMapBuilder();

        protected abstract IGameBoard BuildAndGetGameBoard(IGameBoardMapBuilder gameBoardMapBuilder);

        protected abstract IShape BuildAndGetShape(ShapeTypes shapeType,
            Point startPosition,
            IGameBoard gameBoard,
            IMoveStrategy moveStrategy,
            IPathAnalyzer pathAnalyzer,
            ISurvivalChecker survivalChecker);

        protected abstract IPathAnalyzer GetPathAnalyzerForShape(ShapeTypes shapeType, ISurvivalChecker survivalChecker);        
        
        protected abstract IMoveStrategy GetMoveStrategyForShape(ShapeTypes shapeType);

        protected abstract ISurvivalChecker GetSurvivalChecker(ShapeTypes shapeType);        
    }
}
