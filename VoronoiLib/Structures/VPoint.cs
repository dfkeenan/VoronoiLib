using System.Diagnostics;

namespace VoronoiLib.Structures
{
    [DebuggerDisplay("({X},{Y})")]
    public class VPoint
    {
        public double X { get; }
        public double Y { get; }

        public VPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool ApproxEqual(VPoint other)
            => X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y);
    }
}
