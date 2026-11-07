using System;
using System.Collections.Generic;
using ConvexHull.Models;

namespace ConvexHull.Services;

public static class PointGenerator
{
    public static List<Point2D> Generate(
        int amount,
        double canvasWidth,
        double canvasHeight
        )
    {
        var points = new List<Point2D>();
        var random = new Random();

        for (int i = 0; i < amount; i++)
        {
            double x = random.NextDouble() * canvasWidth;
            double y = random.NextDouble() * canvasHeight;

            points.Add(new Point2D(x, y));
        }

        return points;
    }
}