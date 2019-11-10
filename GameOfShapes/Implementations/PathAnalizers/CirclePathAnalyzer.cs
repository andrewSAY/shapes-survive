using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfShapes.Implementations.PathAnalizers
{
    public class CirclePathAnalyzer : PathAnalyzerBase
    {
        protected override int PossibleStepsCountOnMove => 1;

        protected override int PossibleStepsCountByDiagonalOnMove => 0;

        protected override bool ShapeWillSurviveOnCell(IGameBoardCell cell)
        {
            return true;
        }
    }
}
