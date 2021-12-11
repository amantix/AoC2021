using FluentAssertions;
using NUnit.Framework;

namespace Day11;

[TestFixture]
public class Tests
{
    [Test, Timeout(10000)]
    [TestCase("example.txt", 1656)]
    [TestCase("input.txt", 1594)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(10000)]
    [TestCase("example.txt", 195)]
    [TestCase("input.txt", 437)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}