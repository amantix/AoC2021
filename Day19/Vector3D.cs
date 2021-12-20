using System.Numerics;

namespace Day19;

public static class Vector3Extensions
{
    public static int CalculateManhattanDistance(this Vector3 vector, Vector3 other)
         => (int) (Math.Abs(other.X - vector.X) + Math.Abs(other.Y - vector.Y) + Math.Abs(other.Z - vector.Z));
    public static IEnumerable<Vector3> GetOrientations(this Vector3 vector)
        => vector.GetDirections().SelectMany(v => v.GetRotations());
    
    private static IEnumerable<Vector3> GetDirections(this Vector3 vector) =>
        new Vector3[]
        {
            new (vector.X, vector.Y, vector.Z), new (-vector.X, -vector.Y, vector.Z),
            new (vector.Y, vector.Z, vector.X), new (-vector.Y, -vector.Z, vector.X),
            new (vector.Z, vector.X, vector.Y), new (-vector.Z, -vector.X, vector.Y)
        };

    private static IEnumerable<Vector3> GetRotations(this Vector3 vector) =>
        new Vector3[]
        {
            new(vector.X, vector.Y, vector.Z), new(vector.X, -vector.Z, vector.Y),
            new(vector.X, -vector.Y, -vector.Z), new(vector.X, vector.Z, -vector.Y)
        };
}