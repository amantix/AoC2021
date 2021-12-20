using Day20;
using FluentAssertions;
using NUnit.Framework;

namespace Day10;

[TestFixture]
public class Tests
{
    [Test, Timeout(100000)]
    [TestCase("example.txt", 35)]
    [TestCase("input.txt", 5464)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(100000)]
    [TestCase("example.txt", 3351)]
    [TestCase("input.txt", 19228)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}