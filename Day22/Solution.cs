namespace Day22;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var rebootSteps = File
            .ReadAllLines(fileName)
            .Select(line => line.Split(new[] {" ", "x=", "y=", "z=", "..", ","}, StringSplitOptions.RemoveEmptyEntries))
            .Select(line => (turnOn: line[0] == "on", coordinateRanges: line.Skip(1).Select(int.Parse).ToArray()))
            .Where(step => step.coordinateRanges.All(x => Math.Abs(x) <= 50))
            .ToArray();

        var map = new HashSet<(int x, int y, int z)>();
        foreach (var (turnOn, coordinateRanges) in rebootSteps)
        {
            for (var i = coordinateRanges[0]; i <= coordinateRanges[1]; i++)
            for (var j = coordinateRanges[2]; j <= coordinateRanges[3]; j++)
            for (var k = coordinateRanges[4]; k <= coordinateRanges[5]; k++)
            {
                if (turnOn)
                {
                    map.Add((i, j, k));
                }
                else
                {
                    map.Remove((i, j, k));
                }
            }
        }

        return map.Count;
    }


    public static string PartTwo(string fileName)
    {
        var rebootSteps = File
            .ReadAllLines(fileName)
            .Select(line => line.Split(new[] {" ", "x=", "y=", "z=", "..", ","}, StringSplitOptions.RemoveEmptyEntries))
            .Select(line => (
                turnOn: line[0] == "on",
                cuboid: new Cuboid(int.Parse(line[1]),
                    int.Parse(line[2]),
                    int.Parse(line[3]),
                    int.Parse(line[4]),
                    int.Parse(line[5]),
                    int.Parse(line[6]
                    ))))
            .ToArray();
        var cuboids = new Dictionary<Cuboid, int>();

        foreach (var (turnOn, newCuboid) in rebootSteps)
        {
            var newCuboids = new Dictionary<Cuboid, int>();
            var newSign = turnOn ? 1 : -1;
            foreach (var (cuboid, sign) in cuboids)
            {
                var overlap = cuboid.OverlapWith(newCuboid);
                if (overlap.IsValid())
                {
                    newCuboids[overlap] = newCuboids.GetValueOrDefault(overlap, 0) - sign;
                }
            }

            if (turnOn)
            {
                newCuboids[newCuboid] = newCuboids.GetValueOrDefault(newCuboid, 0) + newSign;
            }

            foreach (var (cuboid, sign) in newCuboids)
            {
                cuboids[cuboid] = cuboids.GetValueOrDefault(cuboid, 0) + sign;
            }
        }

        return cuboids
            .Sum(cuboid => cuboid.Key.Width * cuboid.Key.Height * cuboid.Key.Depth * cuboid.Value)
            .ToString();
    }
}