using System.Text;

namespace Day16;

public class Packet
{
    public long Version { get; private set; }
    public long Number { get; private set; }
    public int Type { get; private set; }
    public List<Packet> SubPackets { get; } = new();
    private int _bitCount;

    public static Packet Create(string inputBits)
    {
        var rootPacket = new Packet();
        rootPacket.Version = ParseNumber(inputBits[..3]);
        inputBits = inputBits[3..];
        rootPacket._bitCount += 3;

        rootPacket.Type = (int) ParseNumber(inputBits[..3]);
        inputBits = inputBits[3..];
        rootPacket._bitCount += 3;

        if (rootPacket.Type == 4)
        {
            var (number, bitCount) = ParseGroupedNumber(inputBits);
            rootPacket.Number = number;
            rootPacket._bitCount += bitCount;
        }
        else
        {
            var lengthTypeId = inputBits[0];
            inputBits = inputBits[1..];
            rootPacket._bitCount++;
            if (lengthTypeId == '0') //next 15 bits represent total length in bits of sub-packets
            {
                var totalLength = (int) ParseNumber(inputBits[..15]);
                inputBits = inputBits[15..];
                rootPacket._bitCount += 15;

                inputBits = inputBits[..totalLength];
                while (inputBits.Length > 0)
                {
                    var subPacket = Create(inputBits);
                    rootPacket.SubPackets.Add(subPacket);
                    inputBits = inputBits[subPacket._bitCount..];
                }

                rootPacket._bitCount += totalLength;
            }
            else //next 11 bits number of sub-packets contained by this packet
            {
                var subPacketsNumber = (int) ParseNumber(inputBits[..11]);
                inputBits = inputBits[11..];
                rootPacket._bitCount += 11;
                for (var i = 0; i < subPacketsNumber; i++)
                {
                    var subPacket = Create(inputBits);
                    rootPacket.SubPackets.Add(subPacket);
                    rootPacket._bitCount += subPacket._bitCount;
                    inputBits = inputBits[subPacket._bitCount..];
                }
            }

            rootPacket.Number = OperatorFunctions[rootPacket.Type](rootPacket.SubPackets);
        }

        return rootPacket;
    }

    private static long ParseNumber(string bits) => Convert.ToInt64(bits, 2);

    private static (long number, int bitsCount) ParseGroupedNumber(string inputBits)
    {
        var bits = new StringBuilder();
        var index = 0;
        var lastGroup = false;
        while (!lastGroup)
        {
            lastGroup = inputBits[index++] == '0';
            bits.Append(inputBits[index..(index + 4)]);
            index += 4;
        }

        return (ParseNumber(bits.ToString()), index);
    }

    private static readonly Dictionary<int, Func<List<Packet>, long>> OperatorFunctions
        = new()
        {
            {0, packets => packets.Sum(packet => packet.Number)},
            {1, packets => packets.Aggregate(1L, (acc, packet) => acc * packet.Number)},
            {2, packets => packets.Min(packet => packet.Number)},
            {3, packets => packets.Max(packet => packet.Number)},
            {5, packets => packets[0].Number > packets[1].Number ? 1 : 0},
            {6, packets => packets[0].Number < packets[1].Number ? 1 : 0},
            {7, packets => packets[0].Number == packets[1].Number ? 1 : 0}
        };

    public static IEnumerable<Packet> Flatten(Packet packet)
    {
        return Enumerable.Empty<Packet>()
            .Append(packet)
            .Concat(packet.SubPackets.SelectMany(Flatten));
    }
}