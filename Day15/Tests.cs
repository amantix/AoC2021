using FluentAssertions;
using NUnit.Framework;

namespace Day15;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 40)]
    [TestCase("input.txt", 602)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(100000)]
    [TestCase("example.txt", 315)]
    [TestCase("input.txt", 2935)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}