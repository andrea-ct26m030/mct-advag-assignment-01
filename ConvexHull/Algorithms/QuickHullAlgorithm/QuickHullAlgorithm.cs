using System;
using System.Collections.Generic;
using ConvexHull.Enums;
using ConvexHull.Models;

namespace ConvexHull.Algorithms;

public class QuickHullAlgorithm : IConvexHullAlgorithm
{
    // Whether the steps should be saved for the visual mode
    private Boolean _calculateSteps;
    // List of steps for visual mode
    private List<AlgorithmStep> _steps = new();
    // Index of the current step for visual mode
    private int _stepIndex = 0;
    // The current state of the convex hull being constructed
    private List<Point2D> _currentHull = new();
    // The list of points to be processed
    private List<Point2D> _points = new();

    // Initializes the algorithm with the given points and operation mode; Clear anything from previous runs
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

    // Main clauclate function
    public IReadOnlyList<Point2D> Calculate()
    {
        // make sure there are at least 3 points to form a convex hull
        if (_points.Count < 3)
        {
            throw new InvalidOperationException("At least 3 points are required to calculate the convex hull.");
        }

        // Find the leftmost and rightmost points to start the hull
        Point2D leftmost = FindLeftmostPoint(_points);
        Point2D rightmost = FindRightmostPoint(_points);

        // push the leftmost and rightmost points to the current hull
        _currentHull = [
            leftmost,
            rightmost
        ];

        // add the initial step if in visual mode
        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        // Split the points into two sets: those above and below the line formed by the leftmost and rightmost points
        List<Point2D> leftSet = new List<Point2D>();
        List<Point2D> rightSet = new List<Point2D>();

        // Iterate through the points and classify them into leftSet or rightSet based on their position relative to the line
        foreach (Point2D p in _points)
        {
            // calculate the cross product to determine the position of point p relative to the line formed by leftmost and rightmost
            double crossProduct = CrossProduct(leftmost, rightmost, p);
            if (crossProduct > 0)
            {
                leftSet.Add(p);
            }
            else if (crossProduct < 0)
            {
                rightSet.Add(p);
            }
        }

        // Calculate the convex hull recursively for both sets of points
        Hull(leftSet, leftmost, rightmost);
        Hull(rightSet, rightmost, leftmost);

        // Add the final step if in visual mode
        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        // return the final convex hull
        return _currentHull;
    }

    // Returns the next step in the algorithm for visual mode; returns null if there are no more steps
    public AlgorithmStep? NextStep()
    {
        if (_stepIndex >= _steps.Count)
            return null;

        return _steps[_stepIndex++];
    }

    // Adds the current state of the convex hull to the list of steps for visual mode
    private void SaveStep(IReadOnlyList<Point2D> hull)
    {
        List<Point2D> snapshot = [.. hull];

        _steps.Add(new AlgorithmStep
        {
            Hull = snapshot
        });
    }

    // Recursively calculates the convex hull for a set of points between two points p1 and p2
    private void Hull(List<Point2D> points, Point2D p1, Point2D p2)
    {
        // if there are no points left to process, return
        if (points.Count == 0)
            return;

        // get the point farthest from the line segment p1-p2
        Point2D farthestPoint = FindFarthestPoint(points, p1, p2);

        // initialize the left and right sets for the next recursive calls
        List<Point2D> leftSet = new List<Point2D>();
        List<Point2D> rightSet = new List<Point2D>();

        // iterate through the points and classify them into leftSet or rightSet based on their position relative to the lines formed by p1-farthestPoint and farthestPoint-p2
        foreach (Point2D p in points)
        {
            // if the cross product between p1, farthestPoint, and p is positive, p is to the left of the line segment p1-farthestPoint
            if (CrossProduct(p1, farthestPoint, p) > 0) 
                leftSet.Add(p);
            // if the cross product between farthestPoint, p2, and p is positive, p is to the left of the line segment farthestPoint-p2
            if (CrossProduct(farthestPoint, p2, p) > 0) 
                rightSet.Add(p);
        }

        // insert the farthest point into the current hull between p1 and p2
        int p1Index = _currentHull.IndexOf(p1);
        _currentHull.Insert(p1Index + 1, farthestPoint);

        // if in visual mode, save the current state of the hull
        if (_calculateSteps)
        {
            SaveStep(_currentHull);
        }

        // recursively calculate the convex hull for the left and right sets
        Hull(leftSet, p1, farthestPoint);
        Hull(rightSet, farthestPoint, p2);
    }

    // Helper function to get the cross product
    private double CrossProduct(Point2D p1, Point2D p2, Point2D p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
    }

    // Helper function to get the distance from a point to a line
    private double Distance(Point2D p1, Point2D p2, Point2D p3)
    {
        return Math.Abs(CrossProduct(p1, p2, p3));
    }

    // Helper function to get the point farthest from the line segment p1-p2 from a list of points
    private Point2D FindFarthestPoint( List<Point2D> points, Point2D p1, Point2D p2)
    {
        // initialize the farthest point and the maximum distance
        Point2D farthestPoint = points[0];
        double maxDistance = Distance(p1, p2, farthestPoint);

        // iterate through the points to find the point with the maximum distance from the line segment p1-p2
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

    // Helper function to find the leftmost point in a list of points
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

    // Helper function to find the rightmost point in a list of points
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