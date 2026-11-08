using System.Collections.Generic;

namespace ConvexHull.Models;

public class AlgorithmStep
{
    public IReadOnlyList<Point2D> Hull { get; init; } = [];
}