using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ConvexHull;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void GeneratePoints_Click(
        object? sender,
        RoutedEventArgs e)
    {
        Console.WriteLine("Generate Points clicked!");
    }

    public void NextStep_Click(
        object? sender,
        RoutedEventArgs e)
    {
        Console.WriteLine("Next Step clicked!");
    }
}