using System.Text;

namespace Day13;

public static class Solution
{
    private enum FoldDirection { X, Y }

    public static int PartOne(string fileName)
    {
        var (dots, folds) = ParseInput(fileName);
        var field = InitializeField(dots);
        field = ProceedFolds(field, folds.Take(1));
        return CountDots(field);
    }

    public static string PartTwo(string fileName)
    {
        var (dots, folds) = ParseInput(fileName);
        var field = InitializeField(dots);
        field = ProceedFolds(field, folds);
        return PrintField(field);
    }

    private static int[][] ProceedFolds(int[][] field, IEnumerable<(FoldDirection foldDirection, int value)> folds)
    {
        foreach (var fold in folds)
        {
            int[][] newField;
            switch (fold.foldDirection)
            {
                case FoldDirection.Y:
                    newField = new int[fold.value][];
                    for (var y = 0; y < fold.value; y++)
                    {
                        newField[y] = new int[field[0].Length];
                    }

                    break;
                case FoldDirection.X:
                    newField = new int[field.Length][];
                    for (var y = 0; y < field.Length; y++)
                    {
                        newField[y] = new int[fold.value];
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var row = 0; row < field.Length; row++)
            {
                if (row == newField.Length)
                {
                    continue;
                }

                var y = row > newField.Length ? 2 * newField.Length - row : row;
                for (var column = 0; column < field[row].Length; column++)
                {
                    if (column == newField[y].Length)
                    {
                        continue;
                    }

                    var x = column > newField[y].Length ? 2 * newField[y].Length - column : column;
                    newField[y][x] += field[row][column];
                }
            }

            field = newField;
        }

        return field;
    }

    private static (List<int[]> dots, List<(FoldDirection foldDirection, int value)> folds) ParseInput(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var dots = new List<int[]>();
        var folds = new List<(FoldDirection, int value)>();
        var rowNumber = 0;
        while (input[rowNumber].Length > 1)
        {
            dots.Add(input[rowNumber].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            rowNumber++;
        }

        rowNumber++;
        while (rowNumber < input.Length)
        {
            var arguments = input[rowNumber][11..].Split('=', StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (!Enum.TryParse<FoldDirection>(arguments[0].ToUpper(), out var foldDirection))
            {
                throw new ArgumentException($"Invalid fold direction {arguments[0]}");
            }

            folds.Add((foldDirection, int.Parse(arguments[1])));
            rowNumber++;
        }

        return (dots, folds);
    }

    private static int[][] InitializeField(List<int[]> dots)
    {
        var maxX = dots.Max(dot => dot[0]);
        var maxY = dots.Max(dot => dot[1]);
        var field = new int[maxY + 1][];
        for (var y = 0; y < field.Length; y++)
        {
            field[y] = new int[maxX + 1];
        }

        foreach (var dot in dots)
        {
            field[dot[1]][dot[0]] = 1;
        }

        return field;
    }

    private static int CountDots(int[][] field)
    {
        var count = 0;
        for (var y = 0; y < field.Length; y++)
        {
            for (var x = 0; x < field[y].Length; x++)
            {
                if (field[y][x] > 0)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static string PrintField(int[][] field)
    {
        var sb = new StringBuilder();
        for (var y = 0; y < field.Length; y++)
        {
            for (var x = 0; x < field[y].Length; x++)
            {
                var value = field[y][x];
                sb.Append(value > 0 ? "#" : ".");
            }
            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }
}