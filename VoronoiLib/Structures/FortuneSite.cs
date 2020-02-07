using System.Collections.Generic;

namespace VoronoiLib.Structures
{
    public class FortuneSite
    {
        public double X { get; }
        public double Y { get; }

        public List<VEdge> Cell { get; private set; }

        public List<FortuneSite> Neighbors { get; private set; }

        public FortuneSite(double x, double y)
        {
            X = x;
            Y = y;
            Cell = new List<VEdge>();
            Neighbors = new List<FortuneSite>();
        }

        public IEnumerable<VPoint> GetPolygon()
        {
            var ed = new Queue<VEdge>(Cell);
            var poly = new List<VPoint>();
            VEdge edge;
            VPoint lastPoint = null;
            do
            {
                edge = ed.Dequeue();
                var start = edge.Start;
                var end = edge.End;

                if (poly.Count == 0)
                {
                    poly.Add(start);
                    poly.Add(end);
                    lastPoint = end;
                    continue;
                }

                if (lastPoint.ApproxEqual(start))
                {
                    poly.Add(end);
                    lastPoint = end;
                }
                else if (lastPoint.ApproxEqual(end))
                {
                    poly.Add(start);
                    lastPoint = start;
                }
                else
                {
                    ed.Enqueue(edge);
                }

            }
            while (ed.Count > 0);

            return poly;
        }
    }
}
