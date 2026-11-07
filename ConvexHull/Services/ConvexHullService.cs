using System;
using System.Collections.Generic;
using ConvexHull.Algorithms;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Services;

public class ConvexHullService
{
    public IReadOnlyList<Point2D> Calculate(
        Algorithm algorithm,
        IReadOnlyList<Point2D> points)
    {
        return algorithm switch
        {
            Algorithm.Andrew => CalculateAndrew(points),
            Algorithm.QuickHull => CalculateQuickHull(points),
            _ => throw new ArgumentOutOfRangeException(
                nameof(algorithm),
                algorithm,
                "Unknown convex hull algorithm.")
        };
    }

    private IReadOnlyList<Point2D> CalculateAndrew(IReadOnlyList<Point2D> points)
    {
        var algorithm = new AndrewsAlgorithm();
        algorithm.Initialize(points);

        return algorithm.Calculate();
    }

    private IReadOnlyList<Point2D> CalculateQuickHull(IReadOnlyList<Point2D> points)
    {
        // QuickHull implementation
        throw new NotImplementedException();
    }
}