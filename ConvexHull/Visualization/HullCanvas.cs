using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using ConvexHull.Enums;
using ConvexHull.Models;
using ConvexHull.Services;

namespace ConvexHull.Visualization;

public class HullCanvas : Control
{
    public const int CanvasWidth = 400;
    public const int CanvasHeight = 400;
    private Boolean _closeHull = true;

    private IReadOnlyList<Point2D> _points = [];
    private IReadOnlyList<Point2D> _hull = [];

    public HullCanvas()
    {
        Width = CanvasWidth;
        Height = CanvasHeight;
    }

    public void SetPoints(IReadOnlyList<Point2D> points)
    {
        _points = points;
        _hull = [];

        InvalidateVisual();
    }

    public void SetHull(IReadOnlyList<Point2D> hull, OperationMode operationMode = OperationMode.Performance)
    {
        _hull = hull;
        _closeHull = operationMode == OperationMode.Performance;

        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        DrawHull(context);
        DrawPoints(context);
    }

    private void DrawPoints(DrawingContext context)
    {
        foreach (Point2D point in _points)
        {
            context.DrawEllipse(
                Brushes.CornflowerBlue,
                null,
                new Point(point.X, point.Y),
                2,
                2);
        }
    }

    private void DrawHull(DrawingContext context)
    {
        if (_hull.Count < 2)
            return;

        var pen = new Pen(Brushes.Red, 2);

        int lineCount = _closeHull ? _hull.Count : _hull.Count - 1;

        for (int index = 0; index < lineCount; index++)
        {
            Point2D current = _hull[index];
            Point2D next = _hull[(index + 1) % _hull.Count];

            context.DrawLine(
                pen,
                new Point(current.X, current.Y),
                new Point(next.X, next.Y));
        }
    }

}