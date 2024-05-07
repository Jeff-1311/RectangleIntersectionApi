using RectangleAPI.Models;

namespace RectangleAPI.Helpers
{
    public class GeometryHelper
    {
        // Check if two segments intersect
        public static bool DoSegmentsIntersect(Segment seg1, Segment seg2)
        {
            // Calculate the orientation of three points
            double orientation(Segment s, double px, double py)
            {
                return (s.Y2 - s.Y1) * (px - s.X2) - (s.X2 - s.X1) * (py - s.Y2);
            }

            // Check if the point (px, py) is on segment s
            bool onSegment(Segment s, double px, double py)
            {
                return px <= Math.Max(s.X1, s.X2) && px >= Math.Min(s.X1, s.X2) &&
                       py <= Math.Max(s.Y1, s.Y2) && py >= Math.Min(s.Y1, s.Y2);
            }

            // Calculate the four orientations needed for general and special cases
            double o1 = orientation(seg1, seg2.X1, seg2.Y1);
            double o2 = orientation(seg1, seg2.X2, seg2.Y2);
            double o3 = orientation(seg2, seg1.X1, seg1.Y1);
            double o4 = orientation(seg2, seg1.X2, seg1.Y2);

            // General case
            if (o1 != 0 && o2 != 0 && o3 != 0 && o4 != 0 &&
                ((o1 > 0) != (o2 > 0)) && ((o3 > 0) != (o4 > 0)))
                return true;

            // Special cases
            if (o1 == 0 && onSegment(seg1, seg2.X1, seg2.Y1)) return true;
            if (o2 == 0 && onSegment(seg1, seg2.X2, seg2.Y2)) return true;
            if (o3 == 0 && onSegment(seg2, seg1.X1, seg1.Y1)) return true;
            if (o4 == 0 && onSegment(seg2, seg1.X2, seg1.Y2)) return true;

            return false;
        }

        // Check if a rectangle is intersecting a segment
        public static bool IsRectangleIntersectingSegment(Rectangle rect, Segment seg)
        {
            // Convert a rectangle into four segments
            Segment left = new Segment { X1 = rect.X1, Y1 = rect.Y1, X2 = rect.X1, Y2 = rect.Y2 };
            Segment right = new Segment { X1 = rect.X2, Y1 = rect.Y1, X2 = rect.X2, Y2 = rect.Y2 };
            Segment top = new Segment { X1 = rect.X1, Y1 = rect.Y1, X2 = rect.X2, Y2 = rect.Y1 };
            Segment bottom = new Segment { X1 = rect.X1, Y1 = rect.Y2, X2 = rect.X2, Y2 = rect.Y2 };

            // Check for segment intersection with each side of the rectangle
            return DoSegmentsIntersect(seg, left) || DoSegmentsIntersect(seg, right) ||
                   DoSegmentsIntersect(seg, top) || DoSegmentsIntersect(seg, bottom);
        }
    }
}
