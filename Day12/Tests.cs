using FluentAssertions;
using NUnit.Framework;

namespace Day12;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("small_example.txt", 10)]
    [TestCase("medium_example.txt", 19)]
    [TestCase("example.txt", 226)]
    [TestCase("input.txt", 3779)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(1000)]
    [TestCase("small_example.txt", 36)]
    [TestCase("medium_example.txt", 103)]
    [TestCase("example.txt", 3509)]
    [TestCase("input.txt", 96988)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}