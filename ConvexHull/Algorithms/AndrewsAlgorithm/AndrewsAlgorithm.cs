using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Services;

public class AndrewsAlgorithm
{
    public List<Point2D> Calculate(
        Algorithm algorithm,
        List<Point2D> points)
    {
        List<Point2D> hull;

        hull = points;

        return hull;
    }

    private List<Point2D> SortPoints(List<Point2D> points)
    {
        return points;
    }
}