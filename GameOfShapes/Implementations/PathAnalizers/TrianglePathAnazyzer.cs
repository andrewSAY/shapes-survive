using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfShapes.Implementations.PathAnalizers
{
    public class TrianglePathAnazyzer : PathAnalyzerBase
    {
        protected override int PossibleStepsCountOnMove => 2;

        protected override int PossibleStepsCountByDiagonalOnMove => 1;

        protected override bool ShapeWillSurviveOnCell(IGameBoardCell cell)
        {
            return true;
        }
    }
}
