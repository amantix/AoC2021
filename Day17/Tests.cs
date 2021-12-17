using Day17;
using FluentAssertions;
using NUnit.Framework;

namespace Day5;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase(20,30,-10,-5, 45)]
    [TestCase(257,286,-101,-57, 5050)]
    public void TestPartOne(int xFrom, int xTo, int yFrom, int yTo, int expectedResult)
    {
        Solution.PartOne(xFrom,xTo, yFrom,yTo).Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase(20,30,-10,-5, 112)]
    [TestCase(257,286,-101,-57, 2223)]
    public void TestPartTwo(int xFrom, int xTo, int yFrom, int yTo, int expectedResult)
    {
        Solution.PartTwo(xFrom,xTo, yFrom,yTo).Should().Be(expectedResult);
    }
}