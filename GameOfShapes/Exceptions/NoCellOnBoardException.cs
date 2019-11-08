using System;
using System.Drawing;

namespace GameOfShapes.Exceptions
{
    public class NoCellOnBoardException : Exception
    {
        public NoCellOnBoardException(Point cellPointer)
            : base($"No cell on board (x:{cellPointer.X} ; y:{cellPointer.Y} )") 
        { }
    }
}
