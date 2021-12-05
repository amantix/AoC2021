namespace Day1;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var input = ParseInput(fileName);
        return CountLargerMeasurements(input);
    }

    public static int PartTwo(string fileName)
    {
        var input = ParseInput(fileName);
        var windows = Enumerable.Range(0, input.Length - 2)
            .Select(index => input.Take(new Range(index, index + 3)).Sum()).ToArray();
        return CountLargerMeasurements(windows);
    }

    public static int[] ParseInput(string fileName)
    {
        return File.ReadAllLines(fileName).Select(int.Parse).ToArray();
    }

    private static int CountLargerMeasurements(int[] measurements)
    {
        return Enumerable.Range(1, measurements.Length - 1)
            .Sum(index => measurements[index] > measurements[index - 1] ? 1 : 0);
    }
}