
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace ConvexHull.Visualization;

public class HullCanvas : Control
{
    // Called by Avalonia when the control needs rendering
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        context.Custom(
            new HullDrawOperation(new Rect(Bounds.Size))
        );
    }

    private class HullDrawOperation : ICustomDrawOperation
    {
        public Rect Bounds { get; }

        public HullDrawOperation(Rect bounds)
        {
            Bounds = bounds;
        }

        public void Render(ImmediateDrawingContext context)
        {
            // Get Avalonia's Skia canvas
            var feature =
                context.TryGetFeature<ISkiaSharpApiLeaseFeature>();

            if (feature is null)
                return;

            using var lease = feature.Lease();
            SKCanvas canvas = lease.SkCanvas;

            // Configure how our points should look
            using var paint = new SKPaint
            {
                Color = SKColors.CornflowerBlue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            };

            // Draw example points
            canvas.DrawCircle(100, 100, 5, paint);
            canvas.DrawCircle(250, 150, 5, paint);
            canvas.DrawCircle(180, 280, 5, paint);
        }

        public bool HitTest(Point point)
            => Bounds.Contains(point);

        public bool Equals(ICustomDrawOperation? other)
            => false;

        public void Dispose() { }
    }
}
