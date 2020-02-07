namespace VoronoiLib.Structures
{
    public class VPoint
    {
        public double X { get; }
        public double Y { get; }

        internal VPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool ApproxEqual(VPoint other)
            => X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y);
    }
}
