using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
    private List<Point2D> points;
    private readonly ConvexHullService _convexHullService = new();
    private IReadOnlyList<Point2D> _currentHull = [];


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
        Console.WriteLine("Next Step clicked!");
    }
    public void Start_Click(object? sender, RoutedEventArgs e)
    {
        int pointCount = (int)(PointCountInput.Value ?? 50);

        IReadOnlyList<Point2D> points = PointGenerator.Generate(
            pointCount,
            HullCanvas.CanvasWidth,
            HullCanvas.CanvasHeight);

        HullCanvasView.SetPoints(points);

        
        IReadOnlyList<Point2D> hull = _convexHullService.Calculate(
            _algorithm,
            points);

        HullCanvasView.SetHull(hull);
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


}
