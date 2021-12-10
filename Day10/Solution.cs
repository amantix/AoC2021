namespace Day10;

public static class Solution
{
    private static readonly Dictionary<char, (char open, int errorCost)> ChunkClosingsMap =
        new()
        {
            {')', (open: '(', errorCost: 3)},
            {']', (open: '[', errorCost: 57)},
            {'}', (open: '{', errorCost: 1197)},
            {'>', (open: '<', errorCost: 25137)},
        };

    private static readonly Dictionary<char, int> ChunkOpeningsMap =
        new()
        {
            {'(', 1},
            {'[', 2},
            {'{', 3},
            {'<', 4}
        };

    public static int PartOne(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        return input.Sum(line=>Check(line).corruptedCost);
    }

    public static long PartTwo(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var scores = input
            .Select(Check)
            .Where(result => result.corruptedCost==0)
            .Select(result => result.incompleteCost)
            .OrderBy(x => x)
            .ToArray();
        return scores[scores.Length / 2];
    }

    private static (int corruptedCost, long incompleteCost) Check(string line)
    {
        var stack = new Stack<char>();
        foreach (var symbol in line)
        {
            switch (symbol)
            {
                case '(':
                case '[':
                case '{':
                case '<':
                    stack.Push(symbol);
                    break;
                case ')':
                case ']':
                case '}':
                case '>':
                    var currentChunk = stack.Pop();
                    var (expectedSymbol, errorCost) = ChunkClosingsMap[symbol];
                    if (expectedSymbol != currentChunk)
                    {
                        return (errorCost, 0);
                    }

                    break;
                default:
                    throw new ArgumentException("line contains incorrect characters " +
                                                $"{line.Where(x => !"([{<>}])".Contains(x))}");
            }
        }

        var totalCost = 0L;
        while (stack.Count > 0)
        {
            totalCost = totalCost * 5 + ChunkOpeningsMap[stack.Pop()];
        }

        return (0, totalCost);
    }
}