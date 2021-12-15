namespace Day15;

public static class Solution
{
    public static long PartOne(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        return CalculateRisk(input);
    }

    public static long PartTwo(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        return CalculateRisk(input, 5);
    }

    private static long CalculateRisk(string[] input, int mapSizeMultiplier = 1)
    {
        var mapSize = input.Length * mapSizeMultiplier;

        var risks = new Dictionary<(int row, int column), int>();
        var directions = new (int rowDelta, int columnDelta)[] {(-1, 0), (1, 0), (0, -1), (0, 1)};
        var queue = new PriorityQueue<(int row, int column, int risk), int>();
        queue.Enqueue((0, 0, 0), 0);
        while (queue.Count > 0)
        {
            var (row, column, risk) = queue.Dequeue();
            if (row < 0 || row >= mapSize || column < 0 || column >= mapSize)
            {
                continue;
            }

            var currentRisk = GetMapValue(input, row, column) + risk;
            if (risks.ContainsKey((row, column)) && currentRisk >= risks[(row, column)])
            {
                continue;
            }

            risks[(row, column)] = currentRisk;

            foreach (var (rowDelta, columnDelta) in directions)
            {
                queue.Enqueue((row + rowDelta, column + columnDelta, currentRisk), currentRisk);
            }
        }

        return risks[(mapSize - 1, mapSize - 1)];
    }

    private static int GetMapValue(string[] input, int row, int column)
    {
        if (row == 0 && column == 0) return 0;
        var inputValue = input[row % input.Length][column % input[0].Length] - '0'
                         + row / input.Length + column / input[0].Length;
        inputValue %= 9;
        return inputValue == 0 ? 9 : inputValue;
    }
}