using System;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfShapes
{
    public interface ISession
    {
        event Action<Dictionary<Point, ShapeTypes>, IEnumerable<(Point, Point)>> ShapeMovedEvent;

        event Action<IShape> SomeShapeWonEvent;

        event Action NoShapeOnBoardHasLeftEvent;

        void PlayRound();
    }
}
