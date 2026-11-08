using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Algorithms;

public interface IConvexHullAlgorithm
{
    void Initialize(IReadOnlyList<Point2D> points, OperationMode operationMode);

    IReadOnlyList<Point2D> Calculate();

    AlgorithmStep? NextStep();
}