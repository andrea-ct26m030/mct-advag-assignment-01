using Avalonia.Media.TextFormatting;

namespace ConvexHull.Models;

public class Point2D
{
    public double X { get; }
    public double Y { get; }

    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }
}