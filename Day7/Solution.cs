namespace Day7;

public static class Solution
{
    //here we choose position to align from input values
    public static int PartOne(string fileName)
    {
        var input = File
            .ReadAllText(fileName)
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        return input
            .Select(position => input.Sum(item => Math.Abs(item - position)))
            .Min();
    }

    //here we choose position to align from [1..maxInputValue] range without any optimization
    public static int PartTwo(string fileName)
    {
        var input = ReadIntArray(fileName);
        var max = input.Max();
        return Enumerable.Range(1, max - 1)
            .Select(position => input.Sum(item =>
            {
                var steps = Math.Abs(item - position);
                //calculate cost as an arithmetic series using Gauss's formula for progression from 1 to |item - position|
                return steps * (steps + 1) / 2;
            }))
            .Min();
    }

    private static int[] ReadIntArray(string fileName)
    {
        return File
            .ReadAllText(fileName)
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}