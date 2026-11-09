#!/usr/bin/env python3
"""Fit Convex Hull test files into a 400x400 coordinate space.

Usage:
  python3 normalize_hull_tests.py ConvexHull_TestCases.zip
  python3 normalize_hull_tests.py /path/to/input-folder --size 400 --padding 10
  python3 normalize_hull_tests.py ConvexHull_TestCases.zip --output converted_tests

Inputs: first line is number of points; remaining lines are x,y.
Output: same format, same filenames, scaled uniformly and centered.
"""
import argparse
import io
from pathlib import Path
import zipfile


def convert(content: str, size: float, padding: float) -> str:
    lines = [line.strip() for line in content.splitlines() if line.strip()]
    if not lines:
        raise ValueError('File is empty (expected point count on first line)')
    count = int(lines[0])
    points = []
    for line in lines[1:]:
        parts = line.split(',')
        if len(parts) != 2:
            raise ValueError(f'Expected x,y, got: {line!r}')
        points.append((float(parts[0].strip()), float(parts[1].strip())))
    if len(points) != count:
        raise ValueError(f'Header says {count} points but file contains {len(points)}')
    if count == 0:
        return '0\n'

    min_x = min(x for x, _ in points)
    max_x = max(x for x, _ in points)
    min_y = min(y for _, y in points)
    max_y = max(y for _, y in points)
    dx, dy = max_x - min_x, max_y - min_y
    usable = size - 2 * padding
    # One uniform scale preserves geometry and aspect ratio.
    # If both axes are constant, center the single position.
    scale = min([usable / extent for extent in (dx, dy) if extent > 0], default=1.0)
    offset_x = (size - dx * scale) / 2
    offset_y = (size - dy * scale) / 2

    def format_number(n: float) -> str:
        # Avoid scientific notation, which some simple import parsers reject.
        return f'{n:.12f}'.rstrip('0').rstrip('.') or '0'

    out = [str(count)]
    for x, y in points:
        xx = (x - min_x) * scale + offset_x
        yy = (y - min_y) * scale + offset_y
        out.append(f'{format_number(xx)},{format_number(yy)}')
    return '\n'.join(out) + '\n'


def main():
    p = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    p.add_argument('source', type=Path, help='Input folder or ZIP of .txt test files')
    p.add_argument('--output', type=Path, default=Path('ConvexHull_TestCases_400x400'))
    p.add_argument('--size', type=float, default=400)
    p.add_argument('--padding', type=float, default=10)
    args = p.parse_args()
    if args.size <= 0 or not (0 <= args.padding < args.size / 2):
        p.error('Require size > 0 and 0 <= padding < size/2')
    args.output.mkdir(parents=True, exist_ok=True)

    if args.source.is_dir():
        files = [(f.name, f.read_text(encoding='utf-8')) for f in sorted(args.source.glob('*.txt')) if f.name[:1].isdigit()]
    elif args.source.suffix.lower() == '.zip':
        with zipfile.ZipFile(args.source) as archive:
            files = [(Path(n).name, archive.read(n).decode('utf-8')) for n in sorted(archive.namelist()) if n.lower().endswith('.txt') and Path(n).name[:1].isdigit()]
    else:
        p.error('source must be a folder or .zip file')

    if not files:
        p.error('No numbered .txt test cases found')
    for name, content in files:
        try:
            result = convert(content, args.size, args.padding)
        except Exception as exc:
            raise ValueError(f'Unable to convert {name}: {exc}') from exc
        (args.output / name).write_text(result, encoding='utf-8')
    print(f'Converted {len(files)} files to {args.output.resolve()}')


if __name__ == '__main__':
    main()
