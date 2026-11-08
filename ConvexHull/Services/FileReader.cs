using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ConvexHull.Models;


namespace ConvexHull.Services;

public static class FileReader
{
    public static IReadOnlyList<Point2D> Read(string filePath)
    {
        // Validate that file actually exists
        if (!File.Exists(filePath))  throw new FileNotFoundException($"The file '{filePath}' does not exist.");
    
        // Get all the lines of the file
        var lines = File.ReadAllLines(filePath);

        // Validate that the file meets the requirments (first entry with number of points correct)
        if (lines.Length == 0) throw new InvalidDataException("The file is empty.");
        if (!int.TryParse(lines[0], out int n)) throw new InvalidDataException("The first line must be an integer representing the number of points.");
        if (n != lines.Length - 1) throw new InvalidDataException($"The number of points specified ({n}) does not match the number of points provided ({lines.Length - 1}).");
        if (n < 3) throw new InvalidDataException("At least 3 points are required to calculate the convex hull.");

        // Parse the points from the file
        var points = new List<Point2D>();
        for ( int i = 1; i <= n; i++)
        {
            // Split the line into parts and validate that there are exactly two coordinates
            var parts = lines[i].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) throw new InvalidDataException($"Line {i + 1} does not contain exactly two coordinates.");

            // Tries to parse coordinates as doubles, needs the invariant culture to ensure that the decimal separator is a dot, not a comma (otherwise it will fail)
            if (!double.TryParse(parts[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double x) || !double.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
            {
                throw new InvalidDataException($"Couldn't parse coordinates on line {i + 1}.");
            }

            // If all conditions are passed, add the coordiantes into the list as a Point2D
            points.Add(new Point2D(x, y));
        }
        return points;
    }
}