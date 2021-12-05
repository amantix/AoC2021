namespace Day2;
public static class Solution
{
    public static int PartOne(string fileName)
    {
        var horizontalPosition = 0;
        var depth = 0;
        var input = File
            .ReadAllLines(fileName);
        foreach (var line in input)
        {
            var splittedCommand = line.Split(' ').ToArray();
            var units = int.Parse(splittedCommand[1]);
            switch (splittedCommand[0])
            {
                case "forward":
                    horizontalPosition += units;
                    break;
                case "down":
                    depth += units;
                    break;
                case "up":
                    depth -= units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return depth * horizontalPosition;
    }

    public static int PartTwo(string fileName)
    {
        var horizontalPosition = 0;
        var depth = 0;
        var aim = 0;
        var input = File
            .ReadAllLines(fileName);
        foreach (var line in input)
        {
            var splittedCommand = line.Split(' ').ToArray();
            var units = int.Parse(splittedCommand[1]);
            switch (splittedCommand[0])
            {
                case "forward":
                    horizontalPosition += units;
                    depth += aim * units;
                    break;
                case "down":
                    aim += units;
                    break;
                case "up":
                    aim -= units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return depth * horizontalPosition;
    }
}