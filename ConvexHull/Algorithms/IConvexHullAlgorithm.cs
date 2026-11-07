using System.Collections.Generic;
using ConvexHull.Models;

namespace ConvexHull.Algorithms;

public interface IConvexHullAlgorithm
{
    void Initialize(IReadOnlyList<Point2D> points);

    IReadOnlyList<Point2D> Calculate();

    AlgorithmStep? NextStep();
}