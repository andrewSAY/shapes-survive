using System;
using System.Collections.Generic;
using System.Linq;
using GameOfShapes.Exceptions;

namespace GameOfShapes.Implementations.Strategies
{
    public class WaveTraceStrategy : IMoveStrategy
    {
        private readonly bool _useDiagonal = true;
        private Dictionary<IGameBoardCell, int?> _waveTrace;
        
        private readonly List<CellNodeMapPositions> diagonalesDirections = new List<CellNodeMapPositions>
        {
            CellNodeMapPositions.NordEast, CellNodeMapPositions.NordWest, CellNodeMapPositions.SouthEast, CellNodeMapPositions.SouthWest
        };
        private bool CheckDirection(CellNodeMapPositions direction) => diagonalesDirections.Contains(direction) ? _useDiagonal : true;

        public WaveTraceStrategy(bool useDiagonalesDirection)
        {
            _useDiagonal = useDiagonalesDirection;
        }

        public IEnumerable<IGameBoardCell> CalculateOptimalCellTrace(IGameBoardCell currentPosition, IGameBoardCell targetPosition, IEnumerable<IGameBoardCell> impassableCells)
        {

            _waveTrace = new Dictionary<IGameBoardCell, int?>();
            PushWave(currentPosition, targetPosition, impassableCells);
            var optimalTrace = RepaireTraceAndGetIt(currentPosition, targetPosition);

            return optimalTrace;
        }

        private void PushWave(IGameBoardCell currentPosition, IGameBoardCell targetPosition, IEnumerable<IGameBoardCell> impassableCells)
        {
            var value = 1;
            _waveTrace.Add(currentPosition, value);
            while (!_waveTrace.ContainsKey(targetPosition))
            {
                var maxTraceValue = _waveTrace.Values.Max();
                if (value - maxTraceValue > 100_000)
                {
                    throw new NoPathException();
                }

                var nodesToAdd = new Dictionary<IGameBoardCell, int?>();
                
                foreach (var traceUnit in _waveTrace)
                {
                    if (!traceUnit.Value.HasValue || traceUnit.Value != value)
                    {
                        continue;
                    }

                    foreach (var node in traceUnit.Key.GetMapNodes())
                    {
                        var cell = node.GetBoardCell();
                        if (!_waveTrace.ContainsKey(cell) 
                            && !nodesToAdd.ContainsKey(cell)
                            && !impassableCells.Contains(cell)
                            && CheckDirection(node.GetMapPosition()))
                        {
                            nodesToAdd.Add(cell, value  + 1);
                        }
                    }
                }

                foreach (var nodeToAdd in nodesToAdd)
                {
                    _waveTrace.Add(nodeToAdd.Key, nodeToAdd.Value);
                }
                value++;
            }
        }

        private List<IGameBoardCell> RepaireTraceAndGetIt(IGameBoardCell currentPosition, IGameBoardCell targetPosition)
        {
            var backTrace = new List<IGameBoardCell>();
            var cell = targetPosition;
            var cellValue = _waveTrace[cell];
            backTrace.Add(cell);
            while (cell != currentPosition)
            {
                foreach (var node in cell.GetMapNodes())
                {
                    var nodeCell = node.GetBoardCell();
                    if (_waveTrace.ContainsKey(nodeCell) && _waveTrace[nodeCell] == cellValue - 1)
                    {
                        cell = nodeCell;
                        cellValue--;
                        backTrace.Add(cell);

                        break;
                    }
                }
            }

            return backTrace;
        }
    }
}
