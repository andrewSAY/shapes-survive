using System;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfShapes
{
    public interface ISession
    {
        event Action<Dictionary<Point, ShapeTypes>> ShapeMovedEvent;

        event Action<IShape> SomeShapeWonEvent;

        event Action NoShapeOnBaordLeftEvent;

        void PlayRound();
    }
}
