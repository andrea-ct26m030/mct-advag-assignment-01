
using System;
using System.Collections.Generic;
using ConvexHull.Algorithms;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Services;

// Class responsible for managing the convex hull calculation process, including selecting the appropriate algorithm and handling operation modes
public class ConvexHullService
{
    // Currently selected algorithm
    private IConvexHullAlgorithm? _algorithm;

    // Initializes the selected algorithm with the given points and operation mode
    public IReadOnlyList<Point2D> Calculate(
        Algorithm algorithmType,
        OperationMode operationMode,
        IReadOnlyList<Point2D> points)
    {
        _algorithm = algorithmType switch
        {
            Algorithm.Andrew => new AndrewsAlgorithm(),
            Algorithm.QuickHull => new QuickHullAlgorithm(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(algorithmType),
                algorithmType,
                "Unknown convex hull algorithm.")
        };

        _algorithm.Initialize(points, operationMode);
        return _algorithm.Calculate();
    }

    // Handles the next step in the convex hull calculation process for visual mode
    public AlgorithmStep? NextStep()
    {
        if (_algorithm is null)
        {
            throw new InvalidOperationException(
                "No algorithm has been initialized.");
        }

        return _algorithm.NextStep();
    }
}
