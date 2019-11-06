using System;

namespace GameOfShapes.Exceptions
{
    public class ImpossibleMoveShapeToNoFreeCellException : Exception
    {
        public ImpossibleMoveShapeToNoFreeCellException(IGameBoardCell from, IGameBoardCell to)
            : base($"From (X:{from.GetPosition().X}; Y: {from.GetPosition().Y}) to (X:{to.GetPosition().X}; Y: {to.GetPosition().Y})")
        { }
    }
}
