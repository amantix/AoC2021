namespace Day16;

public static class Solution
{
    public static long PartOne(string fileName)
    {
        var inputBits = ConvertHexToBinary(File.ReadAllText(fileName));
        var packet = Packet.Create(inputBits);

        var allPackets = Packet.Flatten(packet);
        return allPackets.Sum(p => p.Version);
    }


    public static long PartTwo(string fileName)
    {
        var inputBits = ConvertHexToBinary(File.ReadAllText(fileName));
        var packet = Packet.Create(inputBits);
        return packet.Number;
    }

    public static string ConvertHexToBinary(string hexString)
    {
        var hexMap = new Dictionary<char, string>
        {
            {'0', "0000"},
            {'1', "0001"},
            {'2', "0010"},
            {'3', "0011"},
            {'4', "0100"},
            {'5', "0101"},
            {'6', "0110"},
            {'7', "0111"},
            {'8', "1000"},
            {'9', "1001"},
            {'A', "1010"},
            {'B', "1011"},
            {'C', "1100"},
            {'D', "1101"},
            {'E', "1110"},
            {'F', "1111"},
        };
        return hexString.Select(c => hexMap[c]).Aggregate((acc, cur) => string.Join("", acc, cur));
    }
}