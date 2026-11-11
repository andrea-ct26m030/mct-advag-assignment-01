using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using ConvexHull.Enums;
using ConvexHull.Models;


namespace ConvexHull.Services;


// The function was used by adding a button in MainWindow.axaml(.cs), which is invisible in the final version
public class TestAlgorithms
{
    public static void RunAllTests(string testCasesFolder)
    {
        if (!Directory.Exists(testCasesFolder))
        {
            Console.WriteLine($"Test-case directory not found: {testCasesFolder}");
            return;
        }

        string[] testFiles = Directory.GetFiles(testCasesFolder)
        .Where(file =>
            string.Equals(Path.GetExtension(file), ".txt") ||
            string.Equals(Path.GetExtension(file), ".csv"))
        .OrderBy(file => file)
        .ToArray();

        foreach (string file in testFiles)
        {
            try
            {
                IReadOnlyList<Point2D> points = FileReader.Read(file);

                foreach (Algorithm algorithmType in Enum.GetValues<Algorithm>())
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    ConvexHullService convexHullService = new();
                    IReadOnlyList<Point2D> hull =
                        convexHullService.Calculate(
                            algorithmType,
                            OperationMode.Performance,
                            points);

                    stopwatch.Stop();

                    Console.WriteLine(
                        $"File: {Path.GetFileName(file)}, " +
                        $"Algorithm: {algorithmType}, " +
                        $"Hull Points: {hull.Count}, " +
                        $"Time: {stopwatch.ElapsedMilliseconds} ms");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Skipping {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }
}