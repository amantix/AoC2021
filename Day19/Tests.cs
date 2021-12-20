using Day19;
using FluentAssertions;
using NUnit.Framework;

namespace Day10;

[TestFixture]
public class Tests
{
    [Test, Timeout(100000)]
    [TestCase("example.txt", 79)]
    [TestCase("input.txt", 438)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(100000)]
    [TestCase("example.txt", 3621)]
    [TestCase("input.txt", 11985)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}