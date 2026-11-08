using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;
using System.Linq;
using System.Drawing;

namespace ConvexHull.Algorithms;

public class AndrewsAlgorithm : IConvexHullAlgorithm
{
    private List<Point2D> _points = new();
    private List<AlgorithmStep> _steps = new();
    private int _stepIndex = 0;


    public void Initialize(IReadOnlyList<Point2D> points)
    {
        _points = [.. points];
    }

    public IReadOnlyList<Point2D> Calculate()
    {
        // Sort Points
        List<Point2D> sortedPoints = SortPoints(_points);

        if (sortedPoints.Count <= 2)
            return sortedPoints;

        // Create upper and lower hull
        List<Point2D> upperHull = BuildHull(sortedPoints, true);
        List<Point2D> lowerHull = BuildHull(sortedPoints, false);

        // Fuse both hulls together
        List<Point2D> hull = new(upperHull);
        for (int i = lowerHull.Count - 2; i > 0; i--)
        {
            hull.Add(lowerHull[i]);
        }

        return hull;
    }

    public AlgorithmStep NextStep()
    {
        AlgorithmStep step = _steps[_stepIndex];
        _stepIndex++;

        return step;
    }

    private List<Point2D> BuildHull(List<Point2D> points, bool turnLeft)
    {
        List<Point2D> hull = [points[0], points[1]];

        foreach (Point2D next in points)
        {
            // repeat as long as there are at least two elements in hull
            while (hull.Count >= 2)
            {
                // both last two hull elements
                Point2D secondLast = hull[^2];
                Point2D last = hull[^1];

                // calculate the orientation
                double orientation = Orient2D(
                   secondLast.X, secondLast.Y,
                   last.X, last.Y,
                   next.X, next.Y);

                // when the last hull elemenet is on the hull, dont remove 
                if (turnLeft ? orientation > 0 : orientation < 0)
                    break;

                // When the last hull element is on the right side, remove from hull
                hull.RemoveAt(hull.Count - 1);
                SaveStep(hull);
            }

            // Each point gets added
            hull.Add(next);
            SaveStep(hull);
        }

        return hull;
    }

    private void SaveStep(IReadOnlyList<Point2D> hull)
    {
        _steps.Add(new AlgorithmStep
        {
            Hull = hull.ToList()
        });
    }

    private List<Point2D> SortPoints(List<Point2D> points)
    {
        return points
            .OrderBy(point => point.X)
            .ThenBy(point => point.Y)
            .ToList();
    }

    private static double Orient2D(
    double ax, double ay,
    double bx, double by,
    double cx, double cy)
    {
        return (bx - ax) * (cy - ay)
             - (by - ay) * (cx - ax);
    }
}