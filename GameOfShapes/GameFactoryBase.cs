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
                var analyzer = GetPathAnalyzerForShape(description.Value);
                yield return BuildAndGetShape(description.Value, description.Key, gameBoard, moveStrategy, analyzer);
            }
        }

        protected abstract ISession BuildAndGetSession(IEnumerable<IShape> shapes, IGameBoard gameBoard);

        protected abstract IGameBoardMapBuilder BuildAndGetGameBoardMapBuilder();

        protected abstract IGameBoard BuildAndGetGameBoard(IGameBoardMapBuilder gameBoardMapBuilder);

        protected abstract IShape BuildAndGetShape(ShapeTypes shapeType,
            Point startPosition,
            IGameBoard gameBoard,
            IMoveStrategy moveStrategy,
            PathAnalyzerBase pathAnalyzer);

        protected abstract PathAnalyzerBase GetPathAnalyzerForShape(ShapeTypes shapeType);        
        
        protected abstract IMoveStrategy GetMoveStrategyForShape(ShapeTypes shapeType);       
    }
}
