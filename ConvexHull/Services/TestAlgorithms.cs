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

        if (testFiles.Length == 0)
        {
            Console.WriteLine($"No test-case files found in: {testCasesFolder}");
            return;
        }

        int fileNameWidth = Math.Max(
            "Test case".Length,
            testFiles.Max(file => Path.GetFileName(file).Length));
        int testRunCount = 0;
        Stopwatch totalStopwatch = Stopwatch.StartNew();

        Console.WriteLine();
        Console.WriteLine("Convex Hull Algorithm Tests");
        Console.WriteLine(new string('=', fileNameWidth + 56));
        Console.WriteLine(
            $"{"Test case".PadRight(fileNameWidth)}  {"Input",8}  {"Algorithm",12}  {"Hull",8}  {"Time",14}");
        Console.WriteLine(new string('-', fileNameWidth + 56));

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

                    testRunCount++;

                    Console.WriteLine(
                        $"{Path.GetFileName(file).PadRight(fileNameWidth)}  " +
                        $"{points.Count,8:N0}  " +
                        $"{algorithmType,12}  " +
                        $"{hull.Count,8:N0}  " +
                        $"{FormatElapsedTime(stopwatch.Elapsed),14}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Skipping {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        totalStopwatch.Stop();
        Console.WriteLine(new string('-', fileNameWidth + 56));
        Console.WriteLine(
            $"Completed {testRunCount:N0} runs across {testFiles.Length:N0} test cases " +
            $"in {FormatElapsedTime(totalStopwatch.Elapsed)}.");
        Console.WriteLine();
    }

    private static string FormatElapsedTime(TimeSpan elapsed)
    {
        return $"{elapsed.TotalMilliseconds:N3} ms";
    }
}
