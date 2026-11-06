using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using ConvexHull.Models;

namespace ConvexHull.Visualization;

public class HullCanvas : Control
{

    public const int CanvasWidth = 400;
    public const int CanvasHeight = 400;
    private List<Point2D> points;


    public HullCanvas()
    {
        Width = CanvasWidth;
        Height = CanvasHeight;
        points = GeneratePoints(10);

    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        DrawLine(points[2], points[9], context);
        DrawPoints(points, context);
    }

    private void DrawPoints(List<Point2D> points, DrawingContext ctx)
    {

        for (int i = 0; i < points.Count; i++)
        {
            Point2D point = points[i];

            ctx.DrawEllipse(
                  Brushes.CornflowerBlue,
                  null,
                  new Point(point.X, point.Y),
                  5,
                  5
              );
        }

    }

    private void DrawLine(Point2D fromPoint, Point2D toPoint, DrawingContext ctx)
    {
        ctx.DrawLine(
            new Pen(Brushes.Red, 2),
            new Point(fromPoint.X, fromPoint.Y),
            new Point(toPoint.X, toPoint.Y)
        );
    }

    private List<Point2D> GeneratePoints(int amount)
    {
        var points = new List<Point2D>();
        var random = new Random();

        for (int point = 0; point < amount; point++)
        {
            double x = random.NextDouble() * CanvasWidth;
            double y = random.NextDouble() * CanvasHeight;

            points.Add(new Point2D(x, y));
        }

        return points;
    }
}