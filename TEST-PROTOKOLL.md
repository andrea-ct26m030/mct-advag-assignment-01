# Testprotokoll

## Zusammenfassung

| Kennzahl | Ergebnis |
|---|---:|
| Testdateien | 41 |
| Ausgeführte Algorithmusläufe | 72 |
| Übersprungene Dateien | 5 |
| Gesamtlaufzeit | 202,035 ms |

## Testergebnisse

Alle Zeitangaben sind in Millisekunden angegeben.

| Testfall | Eingabepunkte | Algorithmus | Hüllenpunkte | Zeit (ms) |
|---|---:|---|---:|---:|
| `05_triangle.txt` | 3 | Andrew | 3 | 1,758 |
| `05_triangle.txt` | 3 | QuickHull | 3 | 0,723 |
| `06_triangle_with_interior.txt` | 6 | Andrew | 3 | 0,058 |
| `06_triangle_with_interior.txt` | 6 | QuickHull | 3 | 0,009 |
| `07_square_corners.txt` | 4 | Andrew | 4 | 0,004 |
| `07_square_corners.txt` | 4 | QuickHull | 4 | 0,002 |
| `08_square_interior.txt` | 8 | Andrew | 4 | 0,004 |
| `08_square_interior.txt` | 8 | QuickHull | 4 | 0,002 |
| `09_square_boundary_collinear.txt` | 9 | Andrew | 4 | 0,006 |
| `09_square_boundary_collinear.txt` | 9 | QuickHull | 4 | 0,002 |
| `10_all_horizontal.txt` | 6 | Andrew | 2 | 0,003 |
| `10_all_horizontal.txt` | 6 | QuickHull | 2 | 0,001 |
| `11_all_vertical.txt` | 5 | Andrew | 2 | 0,002 |
| `11_all_vertical.txt` | 5 | QuickHull | 2 | 0,001 |
| `12_all_diagonal.txt` | 5 | Andrew | 2 | 0,002 |
| `12_all_diagonal.txt` | 5 | QuickHull | 2 | 0,001 |
| `13_collinear_duplicates.txt` | 6 | Andrew | 2 | 0,007 |
| `13_collinear_duplicates.txt` | 6 | QuickHull | 2 | 0,002 |
| `14_all_identical.txt` | 12 | Andrew | 2 | 0,004 |
| `14_all_identical.txt` | 12 | QuickHull | 2 | 0,001 |
| `15_duplicate_corners.txt` | 8 | Andrew | 4 | 0,004 |
| `15_duplicate_corners.txt` | 8 | QuickHull | 4 | 0,002 |
| `16_negative_coordinates.txt` | 6 | Andrew | 5 | 0,003 |
| `16_negative_coordinates.txt` | 6 | QuickHull | 5 | 0,002 |
| `17_fractional_coordinates.txt` | 6 | Andrew | 5 | 0,003 |
| `17_fractional_coordinates.txt` | 6 | QuickHull | 5 | 0,002 |
| `18_same_x_extremes.txt` | 7 | Andrew | 4 | 0,003 |
| `18_same_x_extremes.txt` | 7 | QuickHull | 4 | 0,002 |
| `19_same_y_extremes.txt` | 6 | Andrew | 4 | 0,004 |
| `19_same_y_extremes.txt` | 6 | QuickHull | 4 | 0,001 |
| `20_unsorted_zigzag.txt` | 8 | Andrew | 4 | 0,003 |
| `20_unsorted_zigzag.txt` | 8 | QuickHull | 4 | 0,002 |
| `21_concave_u_shape.txt` | 10 | Andrew | 4 | 0,004 |
| `21_concave_u_shape.txt` | 10 | QuickHull | 4 | 0,002 |
| `22_star_shape.txt` | 10 | Andrew | 5 | 0,005 |
| `22_star_shape.txt` | 10 | QuickHull | 5 | 0,003 |
| `23_near_collinear.txt` | 7 | Andrew | 5 | 0,003 |
| `23_near_collinear.txt` | 7 | QuickHull | 5 | 0,002 |
| `24_large_coordinates.txt` | 6 | Andrew | 4 | 0,003 |
| `24_large_coordinates.txt` | 6 | QuickHull | 4 | 0,002 |
| `25_tiny_coordinates.txt` | 5 | Andrew | 4 | 0,003 |
| `25_tiny_coordinates.txt` | 5 | QuickHull | 4 | 0,004 |
| `26_circle_100.txt` | 100 | Andrew | 100 | 0,032 |
| `26_circle_100.txt` | 100 | QuickHull | 100 | 0,061 |
| `27_semicircle_101.txt` | 101 | Andrew | 101 | 0,028 |
| `27_semicircle_101.txt` | 101 | QuickHull | 101 | 0,074 |
| `28_rectangle_boundary_200.txt` | 140 | Andrew | 4 | 0,041 |
| `28_rectangle_boundary_200.txt` | 140 | QuickHull | 4 | 0,016 |
| `29_grid_21x21.txt` | 441 | Andrew | 4 | 0,089 |
| `29_grid_21x21.txt` | 441 | QuickHull | 4 | 0,056 |
| `30_random_square_100.txt` | 100 | Andrew | 10 | 0,029 |
| `30_random_square_100.txt` | 100 | QuickHull | 10 | 0,020 |
| `31_random_square_1000.txt` | 1.000 | Andrew | 20 | 0,311 |
| `31_random_square_1000.txt` | 1.000 | QuickHull | 20 | 0,145 |
| `32_random_square_10000.txt` | 10.000 | Andrew | 28 | 4,035 |
| `32_random_square_10000.txt` | 10.000 | QuickHull | 28 | 1,417 |
| `33_random_disk_1000.txt` | 1.000 | Andrew | 38 | 0,344 |
| `33_random_disk_1000.txt` | 1.000 | QuickHull | 38 | 0,166 |
| `34_circle_1000.txt` | 1.000 | Andrew | 1.000 | 0,824 |
| `34_circle_1000.txt` | 1.000 | QuickHull | 1.000 | 1,323 |
| `35_circle_10000.txt` | 10.000 | Andrew | 10.000 | 58,685 |
| `35_circle_10000.txt` | 10.000 | QuickHull | 10.000 | 77,080 |
| `36_semicircle_1000.txt` | 1.000 | Andrew | 1.000 | 0,860 |
| `36_semicircle_1000.txt` | 1.000 | QuickHull | 1.000 | 1,296 |
| `37_four_corners_many_interior_10000.txt` | 10.000 | Andrew | 4 | 3,781 |
| `37_four_corners_many_interior_10000.txt` | 10.000 | QuickHull | 4 | 1,247 |
| `38_random_collinear_10000.txt` | 10.000 | Andrew | 15 | 4,204 |
| `38_random_collinear_10000.txt` | 10.000 | QuickHull | 17 | 1,214 |
| `39_shuffled_circle_1000.txt` | 1.000 | Andrew | 1.000 | 1,241 |
| `39_shuffled_circle_1000.txt` | 1.000 | QuickHull | 1.000 | 3,039 |
| `40_ellipse_500.txt` | 500 | Andrew | 500 | 0,467 |
| `40_ellipse_500.txt` | 500 | QuickHull | 500 | 0,565 |

## Übersprungene Dateien

| Datei | Grund |
|---|---|
| `01_empty.txt` | Mindestens drei Punkte sind erforderlich. |
| `02_single.txt` | Mindestens drei Punkte sind erforderlich. |
| `03_two_points.txt` | Mindestens drei Punkte sind erforderlich. |
| `04_two_identical.txt` | Mindestens drei Punkte sind erforderlich. |
| `README.txt` | Die erste Zeile enthält keine gültige Punktanzahl. |

## Andrew’s Algorithm

### Laufzeitanalyse

- **Best Case:** \(O(n \log n)\), müssen zuerst sortiert werden.
- **Worst Case:** Kreis, bei dem alle Punkte auf der konvexen Hülle liegen.

Die Punktverteilung verändert die asymptotische Laufzeit des Algorithmus nicht so stark, da sowieso immer zweimal durch die Punkte durchiteriert werden muss, es gibt keine riesige Schwankungsbreite. 
Allerdings macht die Sortierung etwas aus.

## QuickHull

### Laufzeitanalyse

- **Beste Laufzeit:** Viereck
- **Schlechteste Laufzeit:** Kreis

Bei QuickHull beeinflusst die Punktverteilung die Laufzeit deutlich stärker:

- **Best Case:** \(O(n)\) bei günstig aufgeteilten Punkten, beispielsweise wenn nur wenige Punkte die Hülle bilden.
- **Worst Case:** \(O(n^2)\) bei sehr ungünstigen Aufteilungen, wenn verhältnismäßig viele Punkte Teil der Hülle sind.
