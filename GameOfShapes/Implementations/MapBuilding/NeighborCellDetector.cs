using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfShapes.Implementations.MapBuilding
{
    /// <summary>
    /// Finds neighbor cells of the target cell and set them to target cell as map nodes
    /// </summary>
    class NeighborCellDetector
    {
        private readonly Dictionary<Point, IGameBoardCell> _gameBoardCells;
        private readonly IGameBoardCell _targetCell;

        private readonly Dictionary<CellNodeMapPositions, Func<Point, Point>> _neighborsPositionsCalculateAlgorithms = new Dictionary<CellNodeMapPositions, Func<Point, Point>>
        {
            { CellNodeMapPositions.Nord, new Func<Point, Point>(p => new Point { X = p.X, Y = p.Y - 1 }) },
            { CellNodeMapPositions.NordWest, new Func<Point, Point>(p => new Point { X = p.X - 1, Y = p.Y - 1 }) },
            { CellNodeMapPositions.NordEast, new Func<Point, Point>(p => new Point { X = p.X + 1, Y = p.Y - 1 }) },
            { CellNodeMapPositions.South, new Func<Point, Point>(p => new Point { X = p.X , Y = p.Y + 1 }) },
            { CellNodeMapPositions.SouthWest, new Func<Point, Point>(p => new Point { X = p.X - 1, Y = p.Y + 1 }) },
            { CellNodeMapPositions.SouthEast, new Func<Point, Point>(p => new Point { X = p.X + 1, Y = p.Y + 1 }) },
            { CellNodeMapPositions.West, new Func<Point, Point>(p => new Point { X = p.X - 1, Y = p.Y}) },
            { CellNodeMapPositions.East, new Func<Point, Point>(p => new Point { X = p.X + 1, Y = p.Y}) },
        };

        private readonly Dictionary<CellNodeMapPositions, CellNodeMapPositions> _oppositionsMap = new Dictionary<CellNodeMapPositions, CellNodeMapPositions>
        {
            { CellNodeMapPositions.Nord, CellNodeMapPositions.South }, { CellNodeMapPositions.South, CellNodeMapPositions.Nord},
            { CellNodeMapPositions.East, CellNodeMapPositions.West }, { CellNodeMapPositions.West, CellNodeMapPositions.East},
            { CellNodeMapPositions.NordEast, CellNodeMapPositions.SouthWest }, { CellNodeMapPositions.SouthWest, CellNodeMapPositions.NordEast},
            { CellNodeMapPositions.NordWest, CellNodeMapPositions.SouthEast }, { CellNodeMapPositions.SouthEast, CellNodeMapPositions.NordWest},
        };

        public NeighborCellDetector(IGameBoardCell targetCell, Dictionary<Point, IGameBoardCell> gameBoardCells)
        {
            _gameBoardCells = gameBoardCells;
            _targetCell = targetCell;
        }

        public void DetectAndSetNeighbors()
        {
            foreach (var neighborsPositionsCalculateAlgorithm in _neighborsPositionsCalculateAlgorithms)
            {
                var searchedPoint = neighborsPositionsCalculateAlgorithm.Value(_targetCell.GetPosition());

                if (!_gameBoardCells.ContainsKey(searchedPoint))
                {
                    continue;
                }

                var searchedCell = _gameBoardCells[searchedPoint];
                _targetCell.SetMapNode(new GameBoardCellMapNode(searchedCell, neighborsPositionsCalculateAlgorithm.Key));
                searchedCell.SetMapNode(new GameBoardCellMapNode(_targetCell, _oppositionsMap[neighborsPositionsCalculateAlgorithm.Key]));
            }
        }
    }
}
