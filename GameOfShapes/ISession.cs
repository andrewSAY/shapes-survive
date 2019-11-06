using System;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfShapes
{
    public interface ISession
    {
        event Action<Dictionary<ShapeTypes, Point>> ShapeMovedEvent;

        void PlayRound();
    }
}
