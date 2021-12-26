using FluentAssertions;
using NUnit.Framework;

namespace Day24;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("input.txt", "99919692496939")]
    public void TestPartOne(string fileName, string expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("input.txt", "81914111161714")]
    public void TestPartTwo(string fileName, string expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}