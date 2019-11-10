using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfShapes.Implementations.PathAnalizers
{
    public class SquarePathAnalyzer : PathAnalyzerBase
    {
        protected override int PossibleStepsCountOnMove => 2;

        protected override int PossibleStepsCountByDiagonalOnMove => 2;

        protected override bool ShapeWillSurviveOnCell(IGameBoardCell cell)
        {
            return true;
        }
    }
}
