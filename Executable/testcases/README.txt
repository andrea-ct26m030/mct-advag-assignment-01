Convex Hull Test Cases - normalized for 400x400 canvas

All coordinates are within [10, 390] for each axis.
Uniform scaling preserves the original aspect ratio and relative geometry.
Shapes are centered, so thin/collinear datasets intentionally will not fill both dimensions.

Format: first line = point count, following lines = x,y (dot decimal).

CAUTION: Use original coordinates rather than these transformed data files when comparing
numerical stability or benchmarking original distributions. Normalization can introduce
rounding differences, especially for collinear or very small-coordinate tests.

To re-run:
python3 normalize_hull_tests.py ConvexHull_TestCases.zip --size 400 --padding 10
