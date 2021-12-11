namespace Day11;

public static class Solution
{
    private static (int dx, int dy)[] neighbors =
        {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};

    public static int PartOne(string fileName)
    {
        var octopuses = ReadOctopusesGrid(fileName);

        const int stepsCount = 100;
        var flashesCount = 0;
        for (var step = 1; step <= stepsCount; step++)
        {
            var stepFlashes = SimulateStep(octopuses);
            flashesCount += stepFlashes;
        }

        return flashesCount;
    }

    public static long PartTwo(string fileName)
    {
        var octopuses = File
            .ReadAllLines(fileName)
            .Select(line => line.Select(digit => digit - '0').ToArray())
            .ToArray();

        var flashesCount = 0;
        var step = 0;
        while (flashesCount != 100)
        {
            flashesCount = SimulateStep(octopuses);
            step++;
        }

        return step;
    }

    private static int[][] ReadOctopusesGrid(string fileName)
    {
        return File
            .ReadAllLines(fileName)
            .Select(line => line.Select(digit => digit - '0').ToArray())
            .ToArray();
    }

    /*
    * First, the energy level of each octopus increases by 1.
    * Then, any octopus with an energy level greater than 9 flashes. 
    This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent. 
    If this causes an octopus to have an energy level greater than 9, it also flashes. 
    This process continues as long as new octopuses keep having their energy level increased beyond 9. 
    (An octopus can only flash at most once per step.)
    * Finally, any octopus that flashed during this step has its energy level set to 0,
    as it used all of its energy to flash.
    */
    private static int SimulateStep(int[][] octopuses)
    {
        var gridSize = octopuses[0].Length;
        var flashes = new HashSet<(int, int)>();
        for (var i = 0; i < gridSize; i++)
        {
            for (var j = 0; j < gridSize; j++)
            {
                octopuses[i][j]++;
            }
        }

        var newFlashes = new List<(int, int)>();
        do
        {
            newFlashes.Clear();
            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    if (octopuses[i][j] > 9 && !flashes.Contains((i, j)))
                    {
                        newFlashes.Add((i, j));
                    }
                }
            }

            foreach (var (i, j) in newFlashes)
            {
                flashes.Add((i, j));
                foreach (var (dx, dy) in neighbors)
                {
                    var x = i + dx;
                    var y = j + dy;
                    if (!flashes.Contains((x, y)) && x >= 0 && x < octopuses.Length && y >= 0 &&
                        y < octopuses[i].Length)
                    {
                        octopuses[x][y]++;
                    }
                }
            }
        } while (newFlashes.Count > 0);

        foreach (var (x, y) in flashes)
        {
            octopuses[x][y] = 0;
        }

        return flashes.Count;
    }
}