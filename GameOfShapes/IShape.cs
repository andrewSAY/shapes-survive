using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfShapes
{
    public interface IShape
    {
        bool IsAlive();

        bool CanConnectWith(IShape shape);

        bool ConnectWith(IShape shape);

        bool IsConnectedWith(IShape shape);

        ShapeTypes GetShapeType();

        Point GetPosition();

        IGameBoardCell NextMove();

        void BreakConnectionsIfCantToSave();
    }
}
