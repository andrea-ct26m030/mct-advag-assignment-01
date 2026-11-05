# Andrew’s Algorithm

Laufzeitanalyse:
- Beste Laufzeit: Halbkreis
- Schlechteste Laufzeit: Kreis (alle Punkte liegen auf der konvexen Hülle)

Bei Andrew’s Algorithm ist der Unterschied zwischen den Punktverteilungen relativ gering, da die Sortierung dominiert. Die Laufzeit beträgt in allen Fällen \(O(n \log n)\).

# Quickhull

Laufzeitanalyse:
- Beste Laufzeit: Viereck
- Schlechteste Laufzeit: Kreis
Wichtig: Bei Andrew’s Algorithm ist der Unterschied zwischen den Punktverteilungen relativ gering, da die Sortierung dominiert. Die Laufzeit beträgt in allen Fällen \(O(n \log n)\).

Bei Quickhull beeinflusst die Punktverteilung die Laufzeit deutlich stärker:
- Best Case: \(O(n)\) bei günstig aufgeteilten Punkten, beispielsweise wenn nur wenige Punkte die Hülle bilden.
- Worst Case: \(O(n^2)\) bei sehr ungünstigen Aufteilungen.