using System.Numerics;

namespace Day19;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var scanners = Parse(input);

        var alignedScanners = Scanner.AlignScanners(scanners);

        var uniquePoints = alignedScanners.Select(s => s.AbsoluteBeacons)
            .Aggregate((a, b) => a.Union(b))
            .ToList();
        return uniquePoints.Count;
    }

    public static long PartTwo(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var scanners = Parse(input);

        var alignedScanners = Scanner.AlignScanners(scanners);

        var locations = alignedScanners.Select(x => x.Position).ToList();
        return Enumerable.Range(0, locations.Count - 1)
            .SelectMany(i => Enumerable.Range(i + 1, locations.Count - i - 1).Select(j => (i, j)))
            .Select(pair => locations[pair.i].CalculateManhattanDistance(locations[pair.j]))
            .Max();
    }

    static IEnumerable<Scanner> Parse(string[] input)
    {
        var scanners = new List<Scanner>();
        var id = 0;

        var beacons = new List<Vector3>();
        foreach (var line in input)
        {
            if (line.StartsWith("---"))
            {
                if (beacons.Count > 0)
                    scanners.AddRange(Scanner.CreateOrientations(id++, beacons));
                beacons.Clear();
            }
            else if (!string.IsNullOrEmpty(line))
            {
                var coordinates = line.Split(',').Select(int.Parse).ToArray();
                beacons.Add(new Vector3(coordinates[0], coordinates[1], coordinates[2]));
            }
        }

        if (beacons.Count > 0)
        {
            scanners.AddRange(Scanner.CreateOrientations(id, beacons));
        }

        return scanners;
    }
}