namespace Day22;

public readonly record struct Cuboid(int MinX, int MaxX, int MinY, int MaxY, int MinZ, int MaxZ)
{
    public long Width => MaxX - MinX + 1;
    public long Height => MaxY - MinY + 1;
    public long Depth => MaxZ - MinZ + 1;

    public bool IsValid()
    {
        return MinX <= MaxX && MinY <= MaxY && MinZ <= MaxZ;
    }

    public Cuboid OverlapWith(Cuboid other)
    {
        var overlapMinX = Math.Max(MinX, other.MinX);
        var overlapMaxX = Math.Min(MaxX, other.MaxX);
        var overlapMinY = Math.Max(MinY, other.MinY);
        var overlapMaxY = Math.Min(MaxY, other.MaxY);
        var overlapMinZ = Math.Max(MinZ, other.MinZ);
        var overlapMaxZ = Math.Min(MaxZ, other.MaxZ);
        return new Cuboid(overlapMinX, overlapMaxX, overlapMinY, overlapMaxY, overlapMinZ, overlapMaxZ);
    }
}