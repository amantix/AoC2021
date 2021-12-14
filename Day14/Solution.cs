namespace Day14;

public static class Solution
{
    public static long PartOne(string fileName)
    {
        var (pairCount, rules) = ParseInput(fileName);

        var stepsCount = 10;
        pairCount = ApplyRules(pairCount, rules, stepsCount);

        var elementCount = CountElements(pairCount);

        return GetMaxMinCountDifference(elementCount);
    }

    public static long PartTwo(string fileName)
    {
        var (pairCount, rules) = ParseInput(fileName);

        var stepsCount = 40;
        pairCount = ApplyRules(pairCount, rules, stepsCount);

        var elementCount = CountElements(pairCount);

        return GetMaxMinCountDifference(elementCount);
    }

    private static (Dictionary<(char left, char right), long> pairCounts, Dictionary<(char left, char right), char> rules) ParseInput(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var template = input[0];
        var pairsCount = new Dictionary<(char left, char right), long>();
        var rules = new Dictionary<(char left, char right), char>();
        for (var i = 2; i < input.Length; i++)
        {
            var ruleLine = input[i].Split(" -> ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            rules.Add((ruleLine[0][0], ruleLine[0][1]), ruleLine[1][0]);
        }

        for (var i = 0; i < template.Length - 1; i++)
        {
            var pair = (template[i], template[i + 1]);
            if (!pairsCount.ContainsKey(pair))
            {
                pairsCount.Add(pair, 0);
            }

            pairsCount[pair]++;
        }

        return (pairsCount, rules);
    }

    private static Dictionary<(char left, char right), long> ApplyRules(
        Dictionary<(char left, char right), long> pairCount,
        Dictionary<(char left, char right), char> rules,
        int stepsCount)
    {
        for (var i = 0; i < stepsCount; i++)
        {
            var nextCount = new Dictionary<(char left, char right), long>(pairCount);
            foreach (var rule in rules)
            {
                if (pairCount.ContainsKey(rule.Key) && pairCount[rule.Key] > 0)
                {
                    var insertRight = (rule.Key.left, rule.Value);
                    var insertLeft = (rule.Value, rule.Key.right);
                    if (!nextCount.ContainsKey(insertLeft))
                    {
                        nextCount.Add(insertLeft, 0);
                    }

                    if (!nextCount.ContainsKey(insertRight))
                    {
                        nextCount.Add(insertRight, 0);
                    }

                    nextCount[insertLeft] += pairCount[rule.Key];
                    nextCount[insertRight] += pairCount[rule.Key];
                    nextCount[rule.Key] -= pairCount[rule.Key];
                }
            }

            pairCount = nextCount;
        }

        return pairCount;
    }

    private static Dictionary<char, long> CountElements(Dictionary<(char left, char right), long> pairCount)
    {
        var elementCount = new Dictionary<char, long>();
        foreach (var pair in pairCount)
        {
            var left = pair.Key.left;
            var right = pair.Key.right;
            var count = pair.Value;
            if (!elementCount.ContainsKey(left))
            {
                elementCount[left] = 0;
            }

            if (!elementCount.ContainsKey(right))
            {
                elementCount[right] = 0;
            }

            elementCount[left] += count;
            elementCount[right] += count;
        }

        return elementCount;
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