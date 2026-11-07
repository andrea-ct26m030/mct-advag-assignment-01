namespace ConvexHull.Models;

public class Line2D
{
    public Point2D A { get; }
    public Point2D B { get; }

    public Line2D(Point2D a, Point2D b)
    {
        A = a;
        B = b;
    }
}