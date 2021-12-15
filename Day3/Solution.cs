namespace Day3;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var report = File.ReadAllLines(fileName);
        var counters = GetBitCount(report);

        var (gammaRate, epsilonRate) = counters
            .Aggregate((gammaRate: 0, epsilonRate: 0),
                (pair, counter)
                    => (pair.gammaRate = pair.gammaRate << 1 | (counter > report.Length / 2 ? 1 : 0),
                        pair.epsilonRate = pair.epsilonRate << 1 | (counter > report.Length / 2 ? 0 : 1)
                    ));

        return gammaRate * epsilonRate;
    }

    public static int PartTwo(string fileName)
    {
        var report = File.ReadAllLines(fileName);
        var oxygenValue = Enumerable.Range(0, report[0].Length)
            .Aggregate(report, (oxygenData, i) => oxygenData.Length > 1
                ? oxygenData.Count(x => x[i] == '1') >= oxygenData.Count(x => x[i] == '0')
                    ? oxygenData.Where(x => x[i] == '1').ToArray()
                    : oxygenData.Where(x => x[i] == '0').ToArray()
                : oxygenData)
            .Single();
        var co2Value = Enumerable.Range(0, report[0].Length)
            .Aggregate(report, (co2Data, i) => co2Data.Length > 1
                ? co2Data.Count(x => x[i] == '0') > co2Data.Count(x => x[i] == '1')
                    ? co2Data.Where(x => x[i] == '1').ToArray()
                    : co2Data.Where(x => x[i] == '0').ToArray()
                : co2Data)
            .Single();

        var (oxygen, co2) = Enumerable.Range(0, report[0].Length)
            .Aggregate((oxygen: 0, co2: 0),
                (pair, i)
                    => (pair.oxygen = pair.oxygen << 1 | (oxygenValue[i] == '1' ? 1 : 0),
                        pair.co2 = pair.co2 << 1 | (co2Value[i] == '1' ? 1 : 0)
                    ));

        return oxygen * co2;
    }

    private static int[] GetBitCount(string[] report)
    {
        return report
            .SelectMany(line => line.Select((digit, index) => (digit, i: index)))
            .GroupBy(pair => pair.i)
            .OrderBy(group => group.Key)
            .Select(group => group.Count(digit => digit.Item1 == '1'))
            .ToArray();
    }
}