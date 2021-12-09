using FluentAssertions;
using NUnit.Framework;

namespace Day9;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 15)]
    [TestCase("input.txt", 524)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(10000)]
    [TestCase("example.txt", 1134)]
    [TestCase("input.txt", 1235430)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}