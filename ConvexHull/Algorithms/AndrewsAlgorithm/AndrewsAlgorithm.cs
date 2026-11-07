using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Algorithms;

public class AndrewsAlgorithm : IConvexHullAlgorithm
{
    private List<Point2D> _points = new();

    public void Initialize(IReadOnlyList<Point2D> points)
    {
        _points = new List<Point2D>(points);
    }

    public IReadOnlyList<Point2D> Calculate()
    {
        List<Point2D> hull = _points;

        // TODO: Implement Andrew's Algorithm

        return hull;
    }

    public AlgorithmStep NextStep()
    {
        // TODO: Implement step-by-step execution
        throw new NotImplementedException();
    }

    private List<Point2D> SortPoints(List<Point2D> points)
    {
        // TODO: Sort points by X, then Y
        return points;
    }
}