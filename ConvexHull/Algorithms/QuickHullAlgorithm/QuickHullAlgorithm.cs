using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Algorithms;

public class QuickHullAlgorithm : IConvexHullAlgorithm
{
    private Boolean _calculateSteps;
    private List<AlgorithmStep> _steps = new();
    private int _stepIndex = 0;
    private List<Point2D> _currentHull = new();

    private List<Point2D> _points = new();

    public void Initialize(
        IReadOnlyList<Point2D> points, 
        OperationMode operationMode)
    {
        _points = [.. points];
        _calculateSteps = operationMode == OperationMode.Visual;

        _currentHull.Clear();

        _steps.Clear();
        _stepIndex = 0;
    }

    public IReadOnlyList<Point2D> Calculate()
    {
        if (_points.Count < 3)
        {
            throw new InvalidOperationException("At least 3 points are required to calculate the convex hull.");
        }

        List<Point2D> hull = _points;

        Point2D leftmost = FindLeftmostPoint(hull);
        Point2D rightmost = FindRightmostPoint(hull);

        _currentHull = [
            leftmost,
            rightmost
        ];

        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        List<Point2D> leftSet = new List<Point2D>();
        List<Point2D> rightSet = new List<Point2D>();

        foreach (Point2D p in hull)
        {
            if (CrossProduct(leftmost, rightmost, p) > 0)
            {
                leftSet.Add(p);
            }
            else if (CrossProduct(leftmost, rightmost, p) < 0)
            {
                rightSet.Add(p);
            }
        }

        Hull(leftSet, leftmost, rightmost);
        Hull(rightSet, rightmost, leftmost);

        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        return _currentHull;
    }

    public AlgorithmStep? NextStep()
    {
        if (_stepIndex >= _steps.Count)
            return null;

        return _steps[_stepIndex++];
    }

    private void SaveStep(IReadOnlyList<Point2D> hull)
    {
        List<Point2D> snapshot = [.. hull];

        _steps.Add(new AlgorithmStep
        {
            Hull = snapshot
        });
    }

    private void Hull(List<Point2D> points, Point2D p1, Point2D p2)
    {
        if (points.Count == 0)
            return;

        Point2D farthestPoint = FindFarthestPoint(points, p1, p2);

        List<Point2D> leftSet = new List<Point2D>();
        List<Point2D> rightSet = new List<Point2D>();

        foreach (Point2D p in points)
        {
            if (CrossProduct(p1, farthestPoint, p) > 0) 
                leftSet.Add(p);
            if (CrossProduct(farthestPoint, p2, p) > 0) 
                rightSet.Add(p);
        }

        int p1Index = _currentHull.IndexOf(p1);
        _currentHull.Insert(p1Index + 1, farthestPoint);

        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        Hull(leftSet, p1, farthestPoint);
        Hull(rightSet, farthestPoint, p2);
    }

    private double CrossProduct(Point2D p1, Point2D p2, Point2D p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
    }

    private double Distance(Point2D p1, Point2D p2, Point2D p3)
    {
        return Math.Abs(CrossProduct(p1, p2, p3));
    }

    private Point2D FindFarthestPoint( List<Point2D> points, Point2D p1, Point2D p2)
    {
        Point2D farthestPoint = points[0];
        double maxDistance = Distance(p1, p2, farthestPoint);

        foreach (Point2D p in points)
        {
            double distance = Distance(p1, p2, p);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestPoint = p;
            }
        }

        return farthestPoint;
    }


    private Point2D FindLeftmostPoint(List<Point2D> point2Ds)
    {
        Point2D leftmost = point2Ds[0];
        foreach (Point2D p in point2Ds)
        {
            if (p.X < leftmost.X)
            {
                leftmost = p;
            }
        }
        return leftmost;
    }

    private Point2D FindRightmostPoint(List<Point2D> point2Ds)
    {
        Point2D rightmost = point2Ds[0];
        foreach (Point2D p in point2Ds)
        {
            if (p.X > rightmost.X)
            {
                rightmost = p;
            }
        }
        return rightmost;
    }

}