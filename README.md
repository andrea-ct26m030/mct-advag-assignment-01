# Convex Hull Visualizer

A C# desktop application for visualizing and comparing two 2D convex hull algorithms.

Built with **.NET**, **Avalonia UI**, and **SkiaSharp**.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/) (optional)
- C# Dev Kit extension for VS Code (recommended)

Verify that .NET is installed:

```bash
dotnet --version
```

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd mct-advag-assignment-01
```

### 2. Restore dependencies

From the repository root, run:

```bash
dotnet restore ./ConvexHull/ConvexHull.csproj
```

This installs the required NuGet packages, including Avalonia and SkiaSharp.

### 3. Run the application

From the repository root:

```bash
dotnet run --project ./ConvexHull/ConvexHull.csproj
```

Alternatively, navigate to the project directory:

```bash
cd ConvexHull
dotnet run
```

## Build

To compile the application without running it:

```bash
dotnet build ./ConvexHull/ConvexHull.csproj
```

For an optimized release build:

```bash
dotnet build ./ConvexHull/ConvexHull.csproj -c Release
```

## Technologies

- **C# / .NET** – Application logic and convex hull algorithms
- **Avalonia UI** – Cross-platform desktop interface
- **SkiaSharp** – 2D graphics rendering