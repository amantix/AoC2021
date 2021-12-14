using FluentAssertions;
using NUnit.Framework;

namespace Day14;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 1588)]
    [TestCase("input.txt", 2223)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(1000)]
    [TestCase("example.txt", 2188189693529)]
    [TestCase("input.txt", 2566282754493)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}