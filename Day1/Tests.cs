using FluentAssertions;
using NUnit.Framework;

namespace Day1;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    public void ParseInputWorkOnExample()
    {
        var expected = new [] {199, 200, 208, 210, 200, 207, 240, 269, 260, 263};
        Solution.ParseInput("example.txt").Should().BeEquivalentTo(expected);
    }

    [Test, Timeout(1000)]
    [TestCase("example.txt", 7)]
    [TestCase("input.txt", 1162)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("example.txt", 5)]
    [TestCase("input.txt", 1190)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}