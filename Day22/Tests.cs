using Day22;
using FluentAssertions;
using NUnit.Framework;

namespace Day2;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("small_example.txt", 39)]
    [TestCase("example.txt", 590784)]
    [TestCase("input.txt", 650099)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("example_part2.txt", "2758514936282235")]
    [TestCase("input.txt", "1254011191104293")]
    public void TestPartTwo(string fileName, string expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}