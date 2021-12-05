namespace Day3;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var report = File.ReadAllLines(fileName);
        var counters = new long[report[0].Length];

        foreach (var line in report)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '1')
                {
                    counters[i]++;
                }
            }
        }

        var gammaRate = 0;
        var epsilonRate = 0;
        var bit = 1;
        for (var i = counters.Length - 1; i >= 0; i--)
        {
            if (counters[i] > report.Length / 2)
            {
                gammaRate += bit;
            }
            else
            {
                epsilonRate += bit;
            }

            bit *= 2;
        }

        return gammaRate * epsilonRate;
    }

    public static int PartTwo(string fileName)
    {
        var report = File.ReadAllLines(fileName);
        var counters = new long[report[0].Length];

        foreach (var line in report)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '1')
                {
                    counters[i]++;
                }
            }
        }

        var oxygenData = report;
        var co2Data = report;

        for (var i = 0; i <= counters.Length; i++)
        {
            if (oxygenData.Length > 1)
            {
                var zeroCount = oxygenData.Count(x => x[i] == '1');
                if (zeroCount >= oxygenData.Length - zeroCount)
                {
                    oxygenData = oxygenData.Where(x => x[i] == '1').ToArray();
                }
                else
                {
                    oxygenData = oxygenData.Where(x => x[i] == '0').ToArray();
                }
            }

            if (co2Data.Length > 1)
            {
                var zeroCount = co2Data.Count(x => x[i] == '0');
                if (zeroCount > co2Data.Length - zeroCount)
                {
                    co2Data = co2Data.Where(x => x[i] == '1').ToArray();
                }
                else
                {
                    co2Data = co2Data.Where(x => x[i] == '0').ToArray();
                }
            }
        }

        var oxygenValue = oxygenData.Single();
        var co2Value = co2Data.Single();
        var bit = 1;
        var oxygen = 0;
        var co2 = 0;
        for (var i = counters.Length - 1; i >= 0; i--)
        {
            if (oxygenValue[i] == '1')
            {
                oxygen += bit;
            }

            if (co2Value[i] == '1')
            {
                co2 += bit;
            }

            bit *= 2;
        }

        return oxygen * co2;
    }
}