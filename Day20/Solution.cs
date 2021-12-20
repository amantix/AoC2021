namespace Day20;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var (enhancementAlgorithm, grid) = ParseInput(fileName);
        grid = Enhance(enhancementAlgorithm, grid, 2);
        return grid.Count();
    }

    public static long PartTwo(string fileName)
    {
        var (enhancementAlgorithm, grid) = ParseInput(fileName);
        grid = Enhance(enhancementAlgorithm, grid, 50);
        return grid.Count();
    }

    private static (byte[] enhancementAlgorithm, SparceMatrix grid) ParseInput(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var enhancementAlgorithm = input[0].Select(x => (byte) (x == '.' ? 0 : 1)).ToArray();

        var grid = new SparceMatrix(0);
        for (var row = 0; row < input.Length - 2; row++)
        {
            for (var col = 0; col < input[2].Length; col++)
            {
                if (input[row + 2][col] == '#')
                {
                    grid[col, row] = 1;
                }
            }
        }

        return (enhancementAlgorithm, grid);
    }

    private static SparceMatrix Enhance(byte[] enhancementAlgorithm, SparceMatrix grid, int stepsCount)
    {
        var swapLight = enhancementAlgorithm[0] == 1;
        for (var iteration = 0; iteration < stepsCount; iteration++)
        {
            var newDefaultValue = swapLight ? 1 - grid.DefaultValue : grid.DefaultValue;
            var nextStep = new SparceMatrix((byte) newDefaultValue);
            var values = grid.ToArray();
            var (minX, maxX, minY, maxY) = values
                .Aggregate((minX: int.MaxValue, maxX: int.MinValue, minY: int.MaxValue, maxY: int.MinValue),
                    (acc, item) =>
                    (
                        Math.Min(acc.minX, item.x),
                        Math.Max(acc.maxX, item.x),
                        Math.Min(acc.minY, item.y),
                        Math.Max(acc.maxY, item.y)
                    ));
            for (var x = minX - 1; x <= maxX + 1; x++)
            {
                for (var y = minY - 1; y <= maxY + 1; y++)
                {
                    var next = enhancementAlgorithm[CalculateIndex(grid, x, y)];
                    nextStep[x, y] = next;
                }
            }

            grid = nextStep;
        }

        return grid;
    }

    private static int CalculateIndex(SparceMatrix grid, int x, int y)
    {
        var value = 0;
        for (var dy = -1; dy <= 1; dy++)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                var gridValue = grid[x + dx, y + dy];
                value = (value << 1) | gridValue;
            }
        }

        return value;
    }
}