namespace Day18;

public static class Solution
{
    public static long PartOne(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var pairs = input.Select(Pair.CreatePair).ToArray();
        var result = pairs.Aggregate((acc, pair) => acc + pair);
        return result.GetMagnitude();
    }

    public static long PartTwo(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var pairs = input
            .Select(Pair.CreatePair)
            .ToArray();
        return pairs
            .SelectMany(left=>pairs
                .Where(right=>right!=left).Select(right=>(left,right)))
            .Where(item => item.left != item.right)
            .Aggregate(0, (acc, pair) => Math.Max(acc, (pair.left + pair.right).GetMagnitude()));
    }
}