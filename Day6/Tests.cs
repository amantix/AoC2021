using FluentAssertions;
using NUnit.Framework;

namespace Day6;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 80, 5934)]
    [TestCase("input.txt", 80,380758)]
    public void TestPartOne(string fileName, int lastDay, int expectedResult)
    {
        Solution.PartOne(fileName, lastDay).Should().Be(expectedResult);
    }
    
    [Test, Timeout(10000)]
    [TestCase("example.txt", 256, 26984457539)]
    [TestCase("example.txt", 80, 5934)]
    [TestCase("input.txt", 256,1710623015163)]
    public void TestPartTwo(string fileName, int lastDay, long expectedResult)
    {
        Solution.PartTwo(fileName, lastDay).Should().Be(expectedResult);
    }
}