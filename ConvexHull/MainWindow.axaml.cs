using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ConvexHull.Enums;
using ConvexHull.Models;
using ConvexHull.Services;
using ConvexHull.Visualization;

namespace ConvexHull;

public partial class MainWindow : Window
{

    // DEFAULT SETTINGS
    private Algorithm _algorithm = Algorithm.Andrew;
    private OperationMode _operationMode = OperationMode.Performance;
    private DataSourceType _dataSource = DataSourceType.Random;
    private int _pointCount = 5;


    // FUNCTIONALITY
    private readonly ConvexHullService _convexHullService = new();
    private IReadOnlyList<Point2D> _currentHull = [];
    private string? _selectedFilePath = null;


    public MainWindow()
    {
        InitializeComponent();
        SetDefaults();
    }

    private void SetDefaults()
    {
        DataSelector.ItemsSource = Enum.GetValues<DataSourceType>();
        OperationSelector.ItemsSource = Enum.GetValues<OperationMode>();
        AlgorithmSelector.ItemsSource = Enum.GetValues<Algorithm>();

        PointCountInput.Value = _pointCount;
        DataSelector.SelectedItem = _dataSource;
        OperationSelector.SelectedItem = _operationMode;
        AlgorithmSelector.SelectedItem = _algorithm;
    }

    public void NextStep_Click(
    object? sender,
    RoutedEventArgs e)
    {
        AlgorithmStep? step = _convexHullService.NextStep();

        if (step is null)
            return;

        HullCanvasView.SetHull(step.Hull, OperationMode.Performance);

        Console.WriteLine("Next Step clicked!");
    }

    public void Start_Click(object? sender, RoutedEventArgs e)
    {

        int pointCount = (int)(PointCountInput.Value ?? 50);

        IReadOnlyList<Point2D> points;

        if (_dataSource == DataSourceType.File)
        {
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                Console.WriteLine("No file selected.");
                return;
            }

            try
            {
                points = FileReader.Read(_selectedFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }
        else
        {
            points = PointGenerator.Generate(
                pointCount,
                HullCanvas.CanvasWidth,
                HullCanvas.CanvasHeight);
        }

        HullCanvasView.SetPoints(points);


        Stopwatch stopwatch = Stopwatch.StartNew();

        IReadOnlyList<Point2D> hull = _convexHullService.Calculate(
            _algorithm,
            _operationMode,
            points
            );

        stopwatch.Stop();
        if (_operationMode == OperationMode.Performance)
        {
            Console.WriteLine($"Convex Hull calculated.");
            Console.WriteLine($"Algorithm used: {_algorithm}");
            Console.WriteLine($"Points in hull: {hull.Count}");
            Console.WriteLine($"Time for calculation: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Hull includes:" + string.Join(", ", hull.Select(point => $"({point.X}, {point.Y})")));


            PerformanceOutput.IsVisible = true;
            PerformanceOutput.Text = $"Convex Hull calculated in {stopwatch.ElapsedMilliseconds} ms. More information in the console output.";
            
            HullCanvasView.SetHull(hull);

            return;
        }
        else
        {
            NextStepButton.IsVisible = true;
        }
    }

    public void Algorithm_SelectionChanged(
     object? sender,
     SelectionChangedEventArgs e)
    {
        if (sender is ComboBox { SelectedItem: Algorithm algorithm })
        {
            _algorithm = algorithm;
            Console.WriteLine(_algorithm);
        }
    }

    public void Data_SelectionChanged(
    object? sender,
    SelectionChangedEventArgs e)
    {
        if (sender is ComboBox { SelectedItem: DataSourceType dataSource })
        {
            _dataSource = dataSource;
            Console.WriteLine(_dataSource);
            if (_dataSource == DataSourceType.File)
            {
                FilePickerButton.IsVisible = true;
                FilePathTextBlock.IsVisible = true;
                FilePathTextBlock.Text = _selectedFilePath ?? "No file selected";
                PointCountInput.IsVisible = false;
            }
            else
            {
                FilePickerButton.IsVisible = false;
                FilePathTextBlock.IsVisible = false;
                PointCountInput.IsVisible = true;
            }
        }
    }

    public void Operation_SelectionChanged(
     object? sender,
     SelectionChangedEventArgs e)
    {
        if (sender is ComboBox { SelectedItem: OperationMode operation })
        {
            _operationMode = operation;
            Console.WriteLine(_operationMode);
            if (_operationMode == OperationMode.Performance)
            {
                NextStepButton.IsVisible = false;
            }
            else
            {
                PerformanceOutput.IsVisible = false;
            }
        }
    }

    public void Count_ValueChanged(
    object? sender,
    NumericUpDownValueChangedEventArgs e)
    {
        if (sender is NumericUpDown input && input.Value is decimal value)
        {
            _pointCount = (int)value;
            Console.WriteLine(_pointCount);
        }
    }

    public async void FilePicker_Click(
        object? sender,
        RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Select a file",
                AllowMultiple = false,
                FileTypeFilter =
                [
                    new FilePickerFileType("Point files")
                    {
                        Patterns = ["*.txt", "*.csv"]
                    }
                ]
            });

        if (files.Count > 0)
        {
            _selectedFilePath = files[0].Path?.LocalPath;
        }


        FilePathTextBlock.Text = _selectedFilePath ?? "No file selected";
    }
<<<<<<< Updated upstream
=======

    public void TestAlgorithms_Click(
        object? sender,
        RoutedEventArgs e)
    {
        string testCasesFolder = Path.Combine(AppContext.BaseDirectory, "testcases");
        TestAlgorithms.RunAllTests(testCasesFolder);
    }
>>>>>>> Stashed changes
}
