using System;
using System.Drawing;

namespace GameOfShapes.Exceptions
{
    public class ClosedConnectionBetweenShapesImpossibleException : Exception
    {
        public ClosedConnectionBetweenShapesImpossibleException(Point pointOne, Point pointTwo)
            : base($"Cant to set connection between (X:{pointOne.X}, Y:{pointOne.Y}) and (X:{pointTwo.X}, Y:{pointTwo.Y})")
        { }
    }
}
