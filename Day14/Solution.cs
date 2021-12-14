namespace Day14;

public static class Solution
{
    public static long PartOne(string fileName)
    {
        var (pairCount, rules) = ParseInput(fileName);
        pairCount = ApplyRules(pairCount, rules, 10);
        var elementCount = CountElements(pairCount);
        return GetMaxMinCountDifference(elementCount);
    }

    public static long PartTwo(string fileName)
    {
        var (pairCount, rules) = ParseInput(fileName);
        pairCount = ApplyRules(pairCount, rules, 40);
        var elementCount = CountElements(pairCount);
        return GetMaxMinCountDifference(elementCount);
    }

    private static (Dictionary<string, long> pairCounts, Dictionary<string, string[]> rules)
        ParseInput(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var rules = input
            .Skip(2)
            .Select(line => line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(line => line[0], line => new[]{$"{line[0][0]}{line[1][0]}", $"{line[1][0]}{line[0][1]}"});

        var template = input.First();
        var pairsCount = Enumerable.Range(0, template.Length - 1)
            .Select(index => $"{template[index]}{template[index+1]}")
            .GroupBy(pair => pair)
            .ToDictionary(group => group.Key, group => group.LongCount());

        return (pairsCount, rules);
    }

    private static Dictionary<string, long> ApplyRules(
        Dictionary<string, long> pairCount,
        Dictionary<string, string[]> rules,
        int stepsCount)
    {
        return Enumerable.Range(1, stepsCount)
            .Aggregate(pairCount, (current, _) =>
                rules
                    .Where(rule => current.ContainsKey(rule.Key) && current[rule.Key] > 0)
                    .SelectMany(rule => rule.Value.Select(str => (str, count: current[rule.Key])))
                    .GroupBy(pair => pair.str)
                    .ToDictionary(group => group.Key, group => group.Sum(x => x.count))
            );
    }

    private static Dictionary<char, long> CountElements(Dictionary<string, long> pairCount)
    {
        return pairCount
            .SelectMany(pair => pair.Key.Select(item => (item, count: pair.Value)))
            .GroupBy(counter => counter.item)
            .ToDictionary(group => group.Key, group => group.Sum(counter => counter.count));
    }

    private static long GetMaxMinCountDifference(Dictionary<char, long> elementCount)
    {
        var maxCount = elementCount.Max(element => element.Value);
        var minCount = elementCount.Min(element => element.Value);
        maxCount = maxCount / 2 + maxCount % 2;
        minCount = minCount / 2 + minCount % 2;
        return maxCount - minCount;
    }
}