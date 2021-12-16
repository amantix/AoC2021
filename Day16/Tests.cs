using FluentAssertions;
using NUnit.Framework;

namespace Day16;

[TestFixture]
public class Tests
{
    [Test, Timeout(1000)]
    [TestCaseSource(nameof(GetTestCases))]
    public void TestDecode(string hexString, string bits, int versionSum)
    {
        Solution.ConvertHexToBinary(hexString).Should().Be(bits);
    }
    
    [Test, Timeout(1000)]
    [TestCaseSource(nameof(GetTestCases))]
    public void TestCreate(string hexString, string bits, int versionSum)
    {
        var packet = Packet.Create(bits);
        Packet.Flatten(packet).Sum(x => x.Version).Should().Be(versionSum);
    }
    
    [Test, Timeout(1000)]
    [TestCaseSource(nameof(GetOperatorTestCases))]
    public void TestOperators(string hexString, int expectedRootPacketNumber)
    {
        var bits = Solution.ConvertHexToBinary(hexString);
        var packet = Packet.Create(bits);
        packet.Number.Should().Be(expectedRootPacketNumber);
    }

    [Test, Timeout(1000)]
    [TestCase("example.txt", 6)]
    [TestCase("input.txt", 960)]
    public void TestPartOne(string fileName, int expectedResult)
    {
        Solution.PartOne(fileName).Should().Be(expectedResult);
    }

    [Test, Timeout(100000)]
    [TestCase("example.txt", 2021)]
    [TestCase("input.txt", 12301926782560L)]
    public void TestPartTwo(string fileName, long expectedResult)
    {
        Solution.PartTwo(fileName).Should().Be(expectedResult);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData("D2FE28", "110100101111111000101000", 6);
        yield return new TestCaseData("38006F45291200", "00111000000000000110111101000101001010010001001000000000", 9);
        yield return new TestCaseData("EE00D40C823060", "11101110000000001101010000001100100000100011000001100000", 14);
        yield return new TestCaseData("8A004A801A8002F478", "100010100000000001001010100000000001101010000000000000101111010001111000", 16);
        yield return new TestCaseData("620080001611562C8802118E34", "01100010000000001000000000000000000101100001000101010110001011001000100000000010000100011000111000110100", 12);
        yield return new TestCaseData("C0015000016115A2E0802F182340", "1100000000000001010100000000000000000001011000010001010110100010111000001000000000101111000110000010001101000000", 23);
        yield return new TestCaseData("A0016C880162017C3686B18A3D4780", "101000000000000101101100100010000000000101100010000000010111110000110110100001101011000110001010001111010100011110000000", 31);
    }

    private static IEnumerable<TestCaseData> GetOperatorTestCases()
    {
        yield return new TestCaseData("C200B40A82", 3);
        yield return new TestCaseData("04005AC33890", 54);
        yield return new TestCaseData("880086C3E88112", 7);
        yield return new TestCaseData("CE00C43D881120", 9);
        yield return new TestCaseData("D8005AC2A8F0", 1);
        yield return new TestCaseData("F600BC2D8F", 0);
        yield return new TestCaseData("9C005AC2F8F0", 0);
        yield return new TestCaseData("9C0141080250320F1802104A08", 1);
    }
}