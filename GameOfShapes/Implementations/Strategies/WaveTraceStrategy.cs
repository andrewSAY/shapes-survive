using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfShapes.Implementations.Strategies
{
    public class WaveTraceStrategy : IMoveStrategy
    {
        private readonly bool _useDiagoanal = true;
        private Dictionary<IGameBoardCell, int?> _waveTrace;
        private IEnumerable<IGameBoardCell> _trace;

        public WaveTraceStrategy(bool useDiagonalDirection)
        {
            _useDiagoanal = useDiagonalDirection;
        }

        public IGameBoardCell CalculateOptimalCell(IShape shape, IGameBoardCell currentPosition, IGameBoardCell targetPosition)
        {

            _waveTrace = new Dictionary<IGameBoardCell, int?>();
            PushWave(currentPosition, targetPosition);
            var optimalTrace = RepaireTrace(currentPosition, targetPosition);

            var itselfCell = optimalTrace.Last();
            optimalTrace.Remove(itselfCell);

            return optimalTrace.Last();
        }

        private void PushWave(IGameBoardCell currentPosition, IGameBoardCell targetPosition)
        {
            var value = 1;
            _waveTrace.Add(currentPosition, value);
            while (!_waveTrace.ContainsKey(targetPosition))
            {
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
                        if (!_waveTrace.ContainsKey(cell) && !nodesToAdd.ContainsKey(cell))
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

        private List<IGameBoardCell> RepaireTrace(IGameBoardCell currentPosition, IGameBoardCell targetPosition)
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
