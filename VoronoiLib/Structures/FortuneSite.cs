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

        public IEnumerable<IEnumerable<VPoint>> GetPolygons(FortuneSite site, double minX, double minY, double maxX, double maxY)
        {
            var result = new List<List<VPoint>>();

            var edges = new Queue<VEdge>(site.Cell);
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
                    result.Add(poly);
                    poly = new List<VPoint>();
                }
                else if (IsEdgePoint(startPoint) && IsEdgePoint(endPoint))
                {
                    if (startPoint.X == endPoint.X || startPoint.Y == endPoint.Y)
                    {
                        //Both on the same side
                        poly.Add(new VPoint(startPoint.X, startPoint.Y));
                    }
                    else
                    {
                        //on a corner
                        double cornerX = startPoint.X == minX || startPoint.X == maxX ? startPoint.X : endPoint.X;
                        double cornerY = startPoint.Y == minY || startPoint.Y == maxY ? startPoint.Y : endPoint.Y;

                        poly.Add(new VPoint(cornerX, cornerY));
                        poly.Add(new VPoint(startPoint.X, startPoint.Y));
                    }

                    result.Add(poly);
                    poly = new List<VPoint>();
                }

            }
            while (edges.Count > 0);

            return result;

            bool IsEdgePoint(VPoint point)
            {
                return point.X == minX || point.X == maxX || point.Y == minY || point.Y == maxY;
            }
        }
    }
}
