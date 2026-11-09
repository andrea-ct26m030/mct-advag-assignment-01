CONVEX HULL TEST FILES

Each numbered .txt file uses exactly the format of TestForHull.txt:
Line 1: number of points
Following lines: x,y using a dot as decimal separator (no spaces or comments)
Example:
3
0,0
5,0
2,4

Import the numbered .txt files individually into your project.
MANIFEST.csv is only for checking test expectations; do not import it as point data.

Expected hull counts assume a minimal convex hull: duplicate points are removed and collinear points along hull edges are omitted, retaining only extreme endpoints. If your implementation deliberately keeps boundary-collinear points, expected hull counts differ. For 0/1/2 unique points, expected hull count equals the number of unique points.

Performance test tips: load files before starting the stopwatch, test both algorithms on exactly the same points, disable visualization during measurement, warm up the runtime, and repeat each test many times.

Circle/ellipse values are rounded to 12 significant digits in the files, and the expected counts were computed on the unrounded generated coordinates; these particular sets remain convex at the listed precision.
