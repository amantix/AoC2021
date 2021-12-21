namespace Day21;

public static class Solution
{
    public static long PartOne(string input)
    {
        var playerPositions = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[4])
            .Select(int.Parse)
            .ToArray();
        var players = new[] {(position: playerPositions[0], score: 0L), (position: playerPositions[1], score: 0L)};
        var number = 1;
        var dieRollCount = 0;
        var hasWinner = false;
        while (!hasWinner)
        {
            for (var i = 0; i < players.Length; i++)
            {
                var (position, score) = players[i];
                position = (position + 3 * number + 2) % 10 + 1;
                score += position;
                players[i] = (position, score);
                number = (number + 2) % 100 + 1;
                dieRollCount += 3;
                if (score < 1000) continue;
                hasWinner = true;
                break;
            }
        }

        return players.Min(player => player.score) * dieRollCount;
    }

    public static long PartTwo(string input)
    {
        var playerPositions = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[4])
            .Select(int.Parse)
            .ToArray();

        var cache = new Dictionary<((int, int), (int, int)), (long, long)>();
        var threeDieRolls = Enumerable.Range(1, 3)
            .SelectMany(die1 => Enumerable.Range(1, 3).Select(die2 => (die1, die2)))
            .SelectMany(pair => Enumerable.Range(1, 3).Select(die3 => (pair.die1, pair.die2, die3)))
            .ToArray();
        var (player1UniverseCount, player2UniverseCount) =
            CountUniverses((playerPositions[0], 0), (playerPositions[1], 0));
        return Math.Max(player1UniverseCount, player2UniverseCount);

        // brute force + memoization.
        // each player has 10 possible positions and 21 possible scores
        // resulting in total number of players combinations = (10 * 21) * (10 * 21) = ~4*10^4
        // for each players combination we need to process 3^3 = 27 possible die rolls
        // that gives us roughly 1*10^6 steps, not bad!
        (long player1UniverseCount, long player2UniverseCount) CountUniverses(
            (int position, int score) player1,
            (int position, int score) player2
        )
        {
            if (player1.score >= 21)
            {
                return (1, 0);
            }

            if (player2.score >= 21)
            {
                return (0, 1);
            }

            if (cache.ContainsKey((player1, player2)))
            {
                return cache[(player1, player2)];
            }

            var result = (player1UniverseCount: 0L, player2UniverseCount: 0L);

            foreach (var (die1, die2, die3) in threeDieRolls)
            {
                var newPosition = (player1.position + die1 + die2 + die3 - 1) % 10 + 1;
                var newScore = player1.score + newPosition;
                var (countPlayer2, countNewPlayer1) = CountUniverses(player2, (newPosition, newScore));
                result = (result.player1UniverseCount + countNewPlayer1, result.player2UniverseCount + countPlayer2);
            }

            cache[(player1, player2)] = result;
            return result;
        }
    }
}