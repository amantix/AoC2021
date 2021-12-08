using FluentAssertions;
using NUnit.Framework;

namespace Day8;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 26)]
    [TestCase("input.txt", 362)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(10000)]
    [TestCase("example.txt", 61229)]
    [TestCase("input.txt",1020159)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(1000)]
    [TestCase("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf", 5353)]
    public void TestDecode(string line, int expectedResult)
    {
        var input = line
            .Split('|', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray();
        Solution.Decode(input[0], input[1]).Should().Be(expectedResult);
    }
}