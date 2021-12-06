namespace Day6;

public static class Solution
{
    //simple straightforward lanternfish population simulation solution
    public static int PartOne(string fileName, int lastDay = 80)
    {
        var lanternFishes = GetInput(fileName);
        for (var day = 0; day < lastDay; day++)
        {
            var currentNumber = lanternFishes.Count;
            for (var i = 0; i < currentNumber; i++)
            {
                if (lanternFishes[i] > 0)
                {
                    lanternFishes[i]--;
                }
                else
                {
                    lanternFishes[i] = 6;
                    lanternFishes.Add(8);
                }
            }
        }

        return lanternFishes.Count;
    }

    //smart solution based on lanternfish state counters
    public static long PartTwo(string fileName, int lastDay = 256)
    {
        var lanternFishes = File
            .ReadAllText(fileName)
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        var counters = new long[9];
        foreach (var lanternFish in lanternFishes)
        {
            counters[lanternFish]++;
        }
        for (var day = 0; day < lastDay; day++)
        {
            var nextCounters = new long[9];
            for (var i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    nextCounters[6] += counters[i];
                    nextCounters[8] += counters[i];
                }
                else
                {
                    nextCounters[i - 1] += counters[i];
                }
            }

            counters = nextCounters;
        }

        return counters.Sum();
    }

    private static List<int> GetInput(string fileName)
    {
        return File
            .ReadAllText(fileName)
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}