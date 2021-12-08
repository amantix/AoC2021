namespace Day8;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var input = File
            .ReadAllLines(fileName)
            .Select(line => line
                .Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToArray()
            )
            .ToArray();

        return input.Sum(line => line[1].Count(signal => new[] {2, 3, 4, 7}.Contains(signal.Length)));
    }

    public static int PartTwo(string fileName)
    {
        var input = File
            .ReadAllLines(fileName)
            .Select(line => line
                .Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToArray()
            )
            .ToArray();

        return input.Sum(line => Decode(line[0], line[1]));
    }

    public static int Decode(string[] patterns, string[] digits)
    {
        var patternByDigits = new string[10];
        var digitsByPatterns = new Dictionary<int, int>();

        //We know that 1,4,7 and 8 have unique number of segments so define them first
        foreach (var pattern in patterns)
        {
            switch (pattern.Length)
            {
                case 2:
                    patternByDigits[1] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 1;
                    break;
                case 3:
                    patternByDigits[7] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 7;
                    break;
                case 4:
                    patternByDigits[4] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 4;
                    break;
                case 7:
                    patternByDigits[8] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 8;
                    break;
            }
        }

        //then using the pattern of 8 we can define 0,9 and 6
        //we can get each of them by removing just one segment from the pattern of 8
        //moreover, we can see that only 9 includes all segments of 4 but 0 and 6 don't
        //0 includes all segments of 7 and 6 does not
        foreach (var pattern in patterns.Where(pattern => pattern.Length == 6))
        {
            var contains4 = !patternByDigits[4].Except(pattern).Any();
            if (contains4)
            {
                patternByDigits[9] = pattern;
                digitsByPatterns[ToNumericCode(pattern)] = 9;
            }
            else
            {
                var contains7 = !patternByDigits[7].Except(pattern).Any();
                if (contains7)
                {
                    patternByDigits[0] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 0;
                }
                else
                {
                    patternByDigits[6] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 6;
                }
            }
        }

        //by using similar logic as above we can define the rest patterns for 3,5 and 2
        //they all have 5 segments
        //only 3 contains all segments of 7
        //5 differs from 6 by one segment and 2 differs from 6 by two segments
        foreach (var pattern in patterns.Where(pattern => pattern.Length == 5))
        {
            var contains7 = !patternByDigits[7].Except(pattern).Any();
            if (contains7)
            {
                patternByDigits[3] = pattern;
                digitsByPatterns[ToNumericCode(pattern)] = 3;
            }
            else
            {
                var diff6 = patternByDigits[6].Except(pattern).Count();
                if (diff6 == 1)
                {
                    patternByDigits[5] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 5;
                }
                else
                {
                    patternByDigits[2] = pattern;
                    digitsByPatterns[ToNumericCode(pattern)] = 2;
                }
            }
        }

        var result = 0;
        var multiplier = 1;
        for (var i = digits.Length - 1; i >= 0; i--)
        {
            var number = digitsByPatterns[ToNumericCode(digits[i])];
            result += number * multiplier;
            multiplier *= 10;
        }

        return result;
    }

    //We need to compare signals in spite of wire ordering
    //so just transform the signal to the set of values on each of 7 wires and take its numeric representation
    private static int ToNumericCode(string signal)
    {
        var wires = new int[7];
        foreach (var wire in signal)
        {
            if (wire < 'h')
            {
                wires[wire - 'a'] = 1;
            }
        }

        var code = 0;
        var bit = 1;
        for (var i = 6; i >= 0; i--)
        {
            code += bit * wires[i];
            bit *= 2;
        }

        return code;
    }
}