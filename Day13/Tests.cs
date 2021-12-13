using FluentAssertions;
using NUnit.Framework;

namespace Day13;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCase("example.txt", 17)]
    [TestCase("input.txt", 847)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(1000)]
    [TestCase("example.txt", "#####\n#...#\n#...#\n#...#\n#####\n.....\n.....")]
    [TestCase("input.txt", "###...##..####.###...##..####..##..###..\n" +
                           "#..#.#..#....#.#..#.#..#.#....#..#.#..#.\n" +
                           "###..#......#..#..#.#....###..#..#.###..\n" +
                           "#..#.#.....#...###..#....#....####.#..#.\n" +
                           "#..#.#..#.#....#.#..#..#.#....#..#.#..#.\n" +
                           "###...##..####.#..#..##..####.#..#.###..")]
    public void TestPartTwo(string fileName, string expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }
}