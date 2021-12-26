namespace Day24;

public static class Solution
{
    public static string PartOne(string fileName)
    {
        var commands = Parse(fileName);
        var (_, maxNumber) = FindNumbers(commands);
        return Evaluate(commands, maxNumber) == 0
            ? string.Join("", maxNumber)
            : string.Empty;
    }

    public static string PartTwo(string fileName)
    {
        var commands = Parse(fileName);
        var (minNumber, _) = FindNumbers(commands);
        return Evaluate(commands, minNumber) == 0
            ? string.Join("", minNumber)
            : string.Empty;
    }

    private static string[][] Parse(string fileName)
    {
        return File
            .ReadAllLines(fileName)
            .Select(line => line.Split().ToArray())
            .ToArray();
    }

    private static (int[] minNumber, int[] maxNumber) FindNumbers(string[][] commands)
    {
        var minNumber = new int[14];
        var maxNumber = new int[14];
        var inputs = new List<(int inputIndex, int value)>();
        for (var inputIndex = 0; inputIndex < 14; inputIndex++)
        {
            var firstLineIndex = inputIndex * 18;
            var lineType = string.Join(" ", commands[firstLineIndex + 4]);
            if (lineType == "div z 1")
            {
                inputs.Add((inputIndex, int.Parse(commands[firstLineIndex + 15][2])));
            }
            else
            {
                var (lastInputIndex, lastInputValue) = inputs[^1];
                var diff = int.Parse(commands[firstLineIndex + 5][2]) + lastInputValue;
                maxNumber[lastInputIndex] = 9 - Math.Max(diff, 0);
                maxNumber[inputIndex] = 9 + Math.Min(diff, 0);
                minNumber[lastInputIndex] = 1 - Math.Min(diff, 0);
                minNumber[inputIndex] = 1 + Math.Max(diff, 0);
                inputs.RemoveAt(inputs.Count - 1);
            }
        }

        return (minNumber, maxNumber);
    }

    static int? Evaluate(string[][] lines, int[] input)
    {
        var currentDigit = 0;
        var registers = new int[] {0, 0, 0, 0}; // w, x, y ,z
        foreach (var line in lines)
        {
            var (command, register, value) = ParseCommandArgs(line);
            switch (command)
            {
                case "inp":
                    registers[register] = input[currentDigit++];
                    break;
                case "add":
                    registers[register] += value;
                    break;
                case "mul":
                    registers[register] *= value;
                    break;
                case "div":
                    if (value == 0)
                        return null;
                    registers[register] /= value;
                    break;
                case "mod":
                    if (registers[register] < 0 || value <= 0)
                        return null;
                    registers[register] %= value;
                    break;
                case "eql":
                    registers[register] = registers[register] == value ? 1 : 0;
                    break;
                default:
                    throw new Exception($"Invalid command: {command}");
            }
        }

        return registers[^1];

        (string command, int register, int value) ParseCommandArgs(string[] line)
        {
            var value = line.Length >= 3
                ? line[2][0] >= 'w' && line[2][0] <= 'z'
                    ? registers[line[2][0] - 'w']
                    : int.Parse(line[2])
                : 0;
            return (line[0], line[1][0] - 'w', value);
        }
    }
}