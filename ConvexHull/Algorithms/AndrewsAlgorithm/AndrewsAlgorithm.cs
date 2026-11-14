using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;
using System.Linq;

namespace ConvexHull.Algorithms;

public class AndrewsAlgorithm : IConvexHullAlgorithm
{
    private Boolean _calculateSteps;
    private List<Point2D> _points = new(); // given points
    private List<Point2D> _upperHull = new(); // save upper hull for steps
    private List<AlgorithmStep> _steps = new(); // saved steps
    private int _stepIndex = 0;

    public void Initialize(
    IReadOnlyList<Point2D> points,
    OperationMode operationMode)
    {
        _points = [.. points];
        _calculateSteps = operationMode == OperationMode.Visual;

        _steps.Clear();
        _upperHull.Clear();
        _stepIndex = 0;
    }

    public IReadOnlyList<Point2D> Calculate()
    {
        // Sort Points
        List<Point2D> sortedPoints = SortPoints(_points);

        if (sortedPoints.Count <= 2)
            return sortedPoints;

        // Create upper and lower hull
        List<Point2D> upperHull = BuildHull(sortedPoints, false);
        if (_calculateSteps) // just save upperHull for visualizing steps
            _upperHull = upperHull;
        List<Point2D> lowerHull = BuildHull(sortedPoints, true);

        return CombineHulls(upperHull, lowerHull);
    }

    // Iterate through all saved steps
    public AlgorithmStep? NextStep()
    {
        if (_stepIndex >= _steps.Count)
            return null;

        return _steps[_stepIndex++];
    }


    private List<Point2D> BuildHull(List<Point2D> points, bool reverse)
    {
        List<Point2D> hull = []; // empty at beginning

        // order points depending if upper or lower hull => first two points are added automatically
        IEnumerable<Point2D> orderedPoints = reverse
            ? points.AsEnumerable().Reverse()
            : points;

        // iterate through all points
        foreach (Point2D next in orderedPoints)
        {
            // when there are more than 2 points on hull
            while (hull.Count >= 2)
            {
                // take two last points on hull
                Point2D secondLast = hull[^2];
                Point2D last = hull[^1];

                // calculate if next point on left or right side of line
                double orientation = Orient2D(
                    secondLast.X, secondLast.Y,
                    last.X, last.Y,
                    next.X, next.Y);

                // if bigger than 0, then next point is right, 
                // previous point is also on hull and doesnt need to be removed, 
                // => so break
                if (orientation > 0)
                    break;

                // otherwise, point is left, 
                // so recently added points isnt on hull anymore
                // => remove the most recently added point from hull
                hull.RemoveAt(hull.Count - 1);

                // just save step if visualized
                if (_calculateSteps)
                    SaveStep(hull);
            }

            // add next point to hull
            hull.Add(next);

            if (_calculateSteps)
                SaveStep(hull);
        }

        return hull;
    }


    private void SaveStep(IReadOnlyList<Point2D> hull)
    {
        List<Point2D> snapshot = _upperHull.Count == 0
            ? [.. hull]
            : CombineHulls(_upperHull, hull);

        _steps.Add(new AlgorithmStep
        {
            Hull = snapshot
        });
    }

    private static List<Point2D> CombineHulls(
        IReadOnlyList<Point2D> upperHull,
        IReadOnlyList<Point2D> lowerHull)
    {
        List<Point2D> combinedHull = [.. upperHull];

        foreach (Point2D point in lowerHull)
        {
            if (!combinedHull.Contains(point))
                combinedHull.Add(point);
        }

        return combinedHull;
    }

    // oder by x axis and then by y
    private List<Point2D> SortPoints(List<Point2D> points)
    {
        return points
            .OrderBy(point => point.X)
            .ThenBy(point => point.Y)
            .ToList();
    }

    // Calculates the determinante
    private static double Orient2D(
    double ax, double ay,
    double bx, double by,
    double cx, double cy)
    {
        return (bx - ax) * (cy - ay)
             - (by - ay) * (cx - ax);
    }
}
