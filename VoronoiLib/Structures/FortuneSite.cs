using System.Collections.Generic;
using System.Linq;

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
            var edges = new Queue<VEdge>(Cell);
            var poly = new List<VPoint>();
            VEdge edge;
            VPoint startPoint = null;
            VPoint endPoint = null;
            do
            {
                edge = edges.Dequeue();
                var start = edge.Start;
                var end = edge.End;

                if (poly.Count == 0)
                {
                    poly.Add(start);
                    poly.Add(end);
                    startPoint = start;
                    endPoint = end;
                    continue;
                }

                if (endPoint.ApproxEqual(start))
                {
                    poly.Add(end);
                    endPoint = end;
                }
                else if (endPoint.ApproxEqual(end))
                {
                    poly.Add(start);
                    endPoint = start;
                }
                else if (startPoint.ApproxEqual(start))
                {
                    poly.Insert(0, end);
                    startPoint = end;
                }
                else if (startPoint.ApproxEqual(end))
                {
                    poly.Insert(0, start);
                    startPoint = start;
                }
                else
                {
                    edges.Enqueue(edge);
                }

                if (startPoint.ApproxEqual(endPoint))
                {
                    return poly;
                }

            }
            while (edges.Count > 0);

            return poly;
        }
    }
}
