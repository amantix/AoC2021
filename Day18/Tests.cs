using FluentAssertions;
using NUnit.Framework;

namespace Day18;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt")]
    [TestCase("input.txt")]
    public void TestParse(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        lines.Select(Pair.CreatePair).Select(pair => pair.ToString()).Should().BeEquivalentTo(lines);
    }
    
    
    [Test, Timeout(1000)]
    [TestCase("[[1,2],[[3,4],5]]", 143)]
    [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]" , 1384)]
    [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]" , 445)]
    [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]" , 791)]
    [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]" , 1137)]
    [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]" , 3488)]
    public void TestGetMagnitude(string input, int expectedResult)
    {
        Pair.CreatePair(input).GetMagnitude().Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]","[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [TestCase("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]","[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
    public void TestAddition(string first, string second, string expectedResult)
    {
        var pair = Pair.CreatePair(first)+Pair.CreatePair(second);
        pair.ToString().Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]",
        "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
    public void TestReduce(string str, string expectedResult)
    {
        var pair = Pair.CreatePair(str);
        Pair.Reduce(pair).ToString().Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("[1,1]\n[2,2]\n[3,3]\n[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
    [TestCase("[1,1]\n[2,2]\n[3,3]\n[4,4]\n[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
    [TestCase("[1,1]\n[2,2]\n[3,3]\n[4,4]\n[5,5]\n[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
    [TestCase("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]\n" +
              "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]\n" +
              "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]\n" +
              "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]\n" +
              "[7,[5,[[3,8],[1,4]]]]\n" +
              "[[2,[2,2]],[8,[8,1]]]\n" +
              "[2,9]\n" +
              "[1,[[[9,3],9],[[9,0],[0,7]]]]\n" +
              "[[[5,[7,4]],7],1]\n" +
              "[[[[4,2],2],6],[8,7]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
    public void TestMultiAddition(string lines, string expectedResult)
    {
        var strings = lines.Split('\n',StringSplitOptions.RemoveEmptyEntries);
        var pairs = strings.Select(Pair.CreatePair).ToArray();
        string.Join("\n", pairs.Select(pair=>pair.ToString())).Should().BeEquivalentTo(lines);
        var result = pairs.Aggregate((acc, pair) => acc + pair);
        result.ToString().Should().Be(expectedResult);
    }
    
    [Test, Timeout(1000)]
    [TestCase("example.txt", 4140)]
    [TestCase("input.txt", 3763)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(1000)]
    [TestCase("example.txt", 3993)]
    [TestCase("input.txt", 4664)]
    public void TestPartTwo(string fileName, int expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}