using FluentAssertions;
using NUnit.Framework;

namespace Day7;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 37)]
    [TestCase("input.txt", 341534)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(10000)]
    [TestCase("example.txt", 168)]
    [TestCase("input.txt",93397632)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}