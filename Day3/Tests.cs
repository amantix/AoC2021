using Day3;
using FluentAssertions;
using NUnit.Framework;

namespace Day3;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 198)]
    [TestCase("input.txt", 2972336)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("example.txt", 230)]
    [TestCase("input.txt", 3368358)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}