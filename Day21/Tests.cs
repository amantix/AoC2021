using Day21;
using FluentAssertions;
using NUnit.Framework;

namespace Day10;

[TestFixture]
public class Tests
{
    [Test, Timeout(100000)]
    [TestCase("Player 1 starting position: 4\nPlayer 2 starting position: 8", 739785)]
    [TestCase("Player 1 starting position: 10\nPlayer 2 starting position: 3", 742257)]
    public void TestPartOne(string input, int expectedResult)
    {
        Solution.PartOne(input).Should().Be(expectedResult);
    }

    [Test, Timeout(100000)]
    [TestCase("Player 1 starting position: 4\nPlayer 2 starting position: 8", 444356092776315L)]
    [TestCase("Player 1 starting position: 10\nPlayer 2 starting position: 3", 93726416205179L)]
    public void TestPartTwo(string input, long expectedResult)
    {
        Solution.PartTwo(input).Should().Be(expectedResult);
    }
}