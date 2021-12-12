namespace Day12;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var cavesMap = ReadCavesMap(fileName);
        return CountPaths(cavesMap);
    }

    public static int PartTwo(string fileName)
    {
        var cavesMap = ReadCavesMap(fileName);
        return CountPaths(cavesMap, true);
    }

    private static int CountPaths(Dictionary<string, List<string>> cavesMap,
        bool allowToVisitSingleSmallCaveTwice = false)
    {
        var paths = new HashSet<string>();
        var queue = new Queue<(string current, string? visitTwice, HashSet<string> visited, List<string> path)>();
        var visitSingleSmallCaveTwice = allowToVisitSingleSmallCaveTwice ? null : string.Empty;
        queue.Enqueue(("start", visitSingleSmallCaveTwice, new HashSet<string>(), new List<string>()));
        while (queue.Count > 0)
        {
            var (current, visitTwice, visited, path) = queue.Dequeue();
            path = new List<string>(path) {current};
            if (current == "end")
            {
                paths.Add(string.Join(',', path));
                continue;
            }

            if (char.IsLower(current[0]))
            {
                if (current != "start" && visitTwice == null)
                {
                    foreach (var next in cavesMap[current]
                                 .Where(next => char.IsUpper(next[0]) || !visited.Contains(next)))
                    {
                        queue.Enqueue((next, current, visited, path));
                    }
                }

                visited = new HashSet<string>(visited) {current};
            }

            foreach (var next in cavesMap[current].Where(next => char.IsUpper(next[0]) || !visited.Contains(next)))
            {
                queue.Enqueue((next, visitTwice, visited, path));
            }
        }

        return paths.Count;
    }

    private static Dictionary<string, List<string>> ReadCavesMap(string fileName)
    {
        var lines = File.ReadAllLines(fileName)
            .Select(line => line.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray());
        var cavesMap = new Dictionary<string, List<string>>();
        foreach (var line in lines)
        {
            if (!cavesMap.ContainsKey(line[0]))
            {
                cavesMap[line[0]] = new List<string>();
            }
            cavesMap[line[0]].Add(line[1]);

            if (!cavesMap.ContainsKey(line[1]))
            {
                cavesMap[line[1]] = new List<string>();
            }
            cavesMap[line[1]].Add(line[0]);
        }
        return cavesMap;
    }
}