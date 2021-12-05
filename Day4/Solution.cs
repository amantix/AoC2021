namespace Day4;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var numbers = FillNumbers(input);
        var boards = FillBoards(input);
        var winnerBoards = GetWinnerBoards(boards, numbers);
        return CalculateResult(winnerBoards, 0);
    }

    public static int PartTwo(string fileName)
    {
        var input = File.ReadAllLines(fileName);
        var numbers = FillNumbers(input);
        var boards = FillBoards(input);
        var winnerBoards = GetWinnerBoards(boards, numbers);
        return CalculateResult(winnerBoards, winnerBoards.Count - 1);
    }

    static int[] FillNumbers(string[] input)
    {
        return input[0].Split(',').Select(int.Parse).ToArray();
    }

    static int[][,] FillBoards(string[] input)
    {
        var boardsCount = input.Length / 6;
        var boards = new int[boardsCount][,];
        for (var i = 0; i < boardsCount; i++)
        {
            boards[i] = new int[5, 5];
        }

        var boardIndex = 0;
        for (int i = 1; i + 5 < input.Length; i += 6)
        {
            for (var rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                var row = input[i + rowIndex + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray();
                for (var columnIndex = 0; columnIndex < 5; columnIndex++)
                {
                    var currentNumber = row[columnIndex];
                    boards[boardIndex][rowIndex, columnIndex] = currentNumber;
                }
            }

            boardIndex++;
        }

        return boards;
    }

    static int CalculateResult(List<(int board, int lastNumber, int unmarkedSum)> winnerBoards, int orderNumber)
    {
        var winnerBoardOrder = orderNumber;
        var winnerNumber = winnerBoards[winnerBoardOrder].lastNumber;
        var winnerBoardUnmarkedSum = winnerBoards[winnerBoardOrder].unmarkedSum;

        var result = winnerBoardUnmarkedSum * winnerNumber;
        return result;
    }

    static List<(int board, int lastNumber, int unmarkedSum)> GetWinnerBoards(int[][,] boards, int[] numbers)
    {
        var boardsCount = boards.Length;
        var boardMarks = new bool[boardsCount][,];
        for (var i = 0; i < boardsCount; i++)
        {
            boardMarks[i] = new bool[5, 5];
        }

        var numbersMap = new List<(int boardIndex, int row, int column)>[100];
        for (var i = 0; i < 100; i++)
        {
            numbersMap[i] = new List<(int boardIndex, int row, int column)>();
        }

        for (var board = 0; board < boardsCount; board++)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    numbersMap[boards[board][i, j]].Add((board, i, j));
                }
            }
        }

        var winnerBoards = new List<(int board, int lastNumber, int unmarkedSum)>();
        var restBoards = Enumerable.Range(0, boardsCount).ToHashSet();
        int lastNumber;
        for (var numberIndex = 0; numberIndex < numbers.Length && restBoards.Count > 0; numberIndex++)
        {
            lastNumber = numbers[numberIndex];
            var mapInfo = numbersMap[lastNumber];
            foreach ((var board, var row, var column) in mapInfo)
            {
                boardMarks[board][row, column] = true;
                var fullRow = true;
                var fullColumn = true;
                for (var k = 0; k < 5; k++)
                {
                    fullRow = fullRow && boardMarks[board][row, k];
                    fullColumn = fullColumn && boardMarks[board][k, column];
                }

                if (fullRow || fullColumn)
                {
                    if (restBoards.Contains(board))
                    {
                        var unmarkedSum = 0;
                        for (var i = 0; i < 5; i++)
                        {
                            for (var j = 0; j < 5; j++)
                            {

                                if (!boardMarks[board][i, j])
                                {
                                    unmarkedSum += boards[board][i, j];
                                }
                            }
                        }

                        winnerBoards.Add((board, lastNumber, unmarkedSum));
                        restBoards.Remove(board);
                    }
                }
            }
        }

        return winnerBoards;
    }
}