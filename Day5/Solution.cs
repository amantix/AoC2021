namespace Day5;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var lines = GetInput(fileName);
        var size = lines.SelectMany(x => x).Max() + 1;
        return CountOverlaps(lines, size);
    }

    public static int PartTwo(string fileName)
    {
        var lines = GetInput(fileName);
        var size = lines.SelectMany(x => x).Max() + 1;
        return CountOverlaps(lines, size, true);
    }

    static int[][] GetInput(string fileName)
    {
        return File
            .ReadAllLines(fileName)
            .Select(line => line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
            .Select(line => line.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)))
            .Select(line => line.Select(int.Parse).ToArray())
            .ToArray();
    }

    static int CountOverlaps(int[][] lines, int size, bool countDiagonals = false)
    {
        var field = new int[size, size];
        foreach (var line in lines)
        {
            if (line[0] == line[2] && line[1] != line[3])
            {
                var from = Math.Min(line[1], line[3]);
                var to = Math.Max(line[1], line[3]);
                for (var y = from; y <= to; y++)
                {
                    field[y, line[0]]++;
                }
            }
            else if (line[1] == line[3] && line[0] != line[2])
            {
                var from = Math.Min(line[0], line[2]);
                var to = Math.Max(line[0], line[2]);
                for (var x = from; x <= to; x++)
                {
                    field[line[1], x]++;
                }
            }
            //in first part we consider only horizontal and vertical lines
            //so we need to skip this section in part two
            else if (countDiagonals && Math.Abs(line[0] - line[2]) == Math.Abs(line[1] - line[3]))
            {
                var delta = Math.Abs(line[0] - line[2]);
                var dx = line[2] - line[0] > 0 ? 1 : -1;
                var dy = line[3] - line[1] > 0 ? 1 : -1;
                var x = line[0];
                var y = line[1];
                for (var k = 0; k <= delta; k++)
                {
                    field[y, x]++;
                    x += dx;
                    y += dy;
                }
            }
        }
        //Diagnostic output for example.txt
        /*
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                Console.Write(field[i, j] > 0 ? field[i, j] : ".");
                //Console.Write(' ');
            }

            Console.WriteLine();
        }
        */

        var result = 0;
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                if (field[i, j] > 1)
                {
                    result++;
                }
            }
        }

        return result;
    }
}