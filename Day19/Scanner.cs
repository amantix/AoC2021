using System.Numerics;

namespace Day19;

public record Scanner
{
    public int Id { get; }
    public Vector3 Position { get; }
    public IEnumerable<Vector3> RelativeBeacons { get; }
    public IEnumerable<Vector3> AbsoluteBeacons { get; }

    private Scanner(int id, IEnumerable<Vector3> relativeBeacons, Vector3 position = default)
    {
        Id = id;
        RelativeBeacons = relativeBeacons.Distinct().ToArray();
        Position = position;
        AbsoluteBeacons = RelativeBeacons.Select(vector => vector + Position).ToArray();
    }

    private Scanner(Scanner other, Vector3 position) : this(other.Id, other.RelativeBeacons, position)
    {
    }

    public static IEnumerable<Scanner> CreateOrientations(int id, IEnumerable<Vector3> beacons)
    {
        return beacons.SelectMany(
                b => b.GetOrientations()
                    .Select((v, i) => (index: i, vector: v))).ToArray()
            .GroupBy(v => v.index, g => g.vector)
            .Select(g => new Scanner(id, g));
    }
    
    public static IEnumerable<Scanner> AlignScanners(IEnumerable<Scanner> scanners)
    {
        var notAligned = scanners.GroupBy(x => x.Id)
            .ToDictionary(g => g.Key, g => g.ToList());
        var aligned = new Dictionary<int, Scanner>();

        aligned.Add(0, notAligned[0].First());
        notAligned.Remove(0);

        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0 && notAligned.Count > 0)
        {
            var id = queue.Dequeue();
            var matches = AlignWithTarget(notAligned.Values, aligned[id]).ToArray();
            foreach (var m in matches)
            {
                aligned[m.Id] = m;
                queue.Enqueue(m.Id);
                notAligned.Remove(m.Id);
            }
        }

        return aligned.Values.ToList();
    }

    private static IEnumerable<Scanner> AlignWithTarget(IEnumerable<List<Scanner>> scanners, Scanner target)
        => scanners.Select(g => g.Select(x => target.AbsoluteBeacons
                    .SelectMany(t => x.RelativeBeacons.Select(s => new Scanner(x, t - s)))
                    .FirstOrDefault(moved => target.AbsoluteBeacons.Intersect(moved.AbsoluteBeacons).Count() >= 12))
                .FirstOrDefault(x => x != null))
            .Where(x => x != null)!;
};