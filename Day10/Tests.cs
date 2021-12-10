using FluentAssertions;
using NUnit.Framework;

namespace Day10;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 26397)]
    [TestCase("input.txt", 318099)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(10000)]
    [TestCase("example.txt", 288957)]
    [TestCase("input.txt", 2389738699)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}