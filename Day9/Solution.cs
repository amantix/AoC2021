namespace Day9;

public static class Solution
{
    //In the first part we need to find low points that are less than all of their adjacent neighbors
    public static int PartOne(string fileName)
    {
        var heightsMap = ReadHeightsMap(fileName);
        return GetLowPoints(heightsMap)
            //we calculate th risk level of a point as 1 + its height
            .Sum(lowPoint => 1 + heightsMap[lowPoint.x][lowPoint.y]);
    }

    public static int PartTwo(string fileName)
    {
        var heightsMap = ReadHeightsMap(fileName);
        var lowPoints = GetLowPoints(heightsMap);

        //Find basins doing depth first traversal from the low points until we get boundaries or the height 9
        var basinSizes = new List<int>();
        foreach (var lowPoint in lowPoints)
        {
            var queue = new Queue<(int x, int y)>();
            var visited = new HashSet<(int x, int y)>();
            queue.Enqueue(lowPoint);
            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                visited.Add((x, y));
                if (x > 0 && !visited.Contains((x - 1, y)) && heightsMap[x - 1][y] < 9)
                {
                    queue.Enqueue((x - 1, y));
                }

                if (x < heightsMap.Length - 1 && !visited.Contains((x + 1, y)) && heightsMap[x + 1][y] < 9)
                {
                    queue.Enqueue((x + 1, y));
                }

                if (y > 0 && !visited.Contains((x, y - 1)) && heightsMap[x][y - 1] < 9)
                {
                    queue.Enqueue((x, y - 1));
                }

                if (y < heightsMap[x].Length - 1 && !visited.Contains((x, y + 1)) && heightsMap[x][y + 1] < 9)
                {
                    queue.Enqueue((x, y + 1));
                }
            }
            basinSizes.Add(visited.Count);
        }

        return basinSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (x, acc) => acc * x);
    }

    private static int[][] ReadHeightsMap(string fileName)
    {
        return File
            .ReadAllLines(fileName)
            .Select(line => line.Select(x => x - '0').ToArray())
            .ToArray();
    }

    //In order to find all low points we compare each position with its neighbors
    private static (int x, int y)[] GetLowPoints(int[][] map)
    {
        var lowPoints = new List<(int x, int y)>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if ((i == 0 || map[i][j] < map[i - 1][j])
                    && (i == map.Length - 1 || map[i][j] < map[i + 1][j])
                    && (j == 0 || map[i][j] < map[i][j - 1])
                    && (j == map[i].Length - 1 || map[i][j] < map[i][j + 1]))
                {
                    lowPoints.Add((i, j));
                }
            }
        }

        return lowPoints.ToArray();
    }
}