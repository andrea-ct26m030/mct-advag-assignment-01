using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Services;

public class ConvexHullService
{
    public List<Point2D> Calculate(
        Algorithm algorithm,
        List<Point2D> points)
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

    private List<Point2D> CalculateAndrew(List<Point2D> points)
    {
        // Call your Andrew implementation
        throw new NotImplementedException();
    }

    private List<Point2D> CalculateQuickHull(List<Point2D> points)
    {
        // Call your QuickHull implementation
        throw new NotImplementedException();
    }
}