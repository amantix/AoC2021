using FluentAssertions;
using NUnit.Framework;

namespace Day2;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 150)]
    [TestCase("input.txt", 1250395)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("example.txt", 900)]
    [TestCase("input.txt", 1451210346)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}