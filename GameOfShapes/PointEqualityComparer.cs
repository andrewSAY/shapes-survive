using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfShapes
{
    public class PointEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point left, Point right)
        {
            return left.X == right.X && left.Y== right.Y;
        }

        public int GetHashCode(Point obj)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + obj.X.GetHashCode();
                hash = hash * 23 + obj.Y.GetHashCode();

                return hash;
            }
        }
    }
}
