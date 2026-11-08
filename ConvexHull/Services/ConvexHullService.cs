
using System;
using System.Collections.Generic;
using ConvexHull.Algorithms;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Services;

public class ConvexHullService
{
    private IConvexHullAlgorithm? _algorithm;

    public IReadOnlyList<Point2D> Calculate(
        Algorithm algorithmType,
        OperationMode operationMode,
        IReadOnlyList<Point2D> points)
    {
        _algorithm = algorithmType switch
        {
            Algorithm.Andrew => new AndrewsAlgorithm(),
            Algorithm.QuickHull => new AndrewsAlgorithm(), // TODO: change to QuickHull() algorithm
            _ => throw new ArgumentOutOfRangeException(
                nameof(algorithmType),
                algorithmType,
                "Unknown convex hull algorithm.")
        };

        _algorithm.Initialize(points, operationMode);
        return _algorithm.Calculate();
    }

    public AlgorithmStep? NextStep()
    {
        if (_algorithm is null)
        {
            throw new InvalidOperationException(
                "No algorithm has been initialized.");
        }

        return _algorithm.NextStep();
    }

    private IReadOnlyList<Point2D> CalculateAndrew(IReadOnlyList<Point2D> points)
    {
        var algorithm = new AndrewsAlgorithm();
        algorithm.Initialize(points);

        return algorithm.Calculate();
    }

    private IReadOnlyList<Point2D> CalculateQuickHull(IReadOnlyList<Point2D> points)
    {
        var algorithm = new QuickHullAlgorithm();
        algorithm.Initialize(points);

        return algorithm.Calculate();
    }
}
