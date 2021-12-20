using System.Collections;

namespace Day20;

public class SparceMatrix : IEnumerable<(int x, int y, byte value)>
{
    public SparceMatrix(byte defaultValue)
    {
        DefaultValue = defaultValue;
    }
    public byte DefaultValue { get; }
    private HashSet<(int x, int y)> pixelsMap = new();

    public byte this[int x, int y]
    {
        get => pixelsMap.Contains((x, y)) ? (byte)(1 - DefaultValue) : DefaultValue;
        set
        {
            if (value == DefaultValue)
            {
                pixelsMap.Remove((x, y));
            }
            else
            {
                pixelsMap.Add((x,y));
            }
        }
    }

    public IEnumerator<(int x, int y, byte value)> GetEnumerator()
    {
        return pixelsMap.Select(item => (item.x, item.y, (byte) (1 - DefaultValue))).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}