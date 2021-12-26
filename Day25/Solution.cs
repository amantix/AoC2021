namespace Day25;

public static class Solution
{
    public static int PartOne(string fileName)
    {
        var seaFloor = SeaFloor.ReadFromFile(fileName);
        seaFloor.SimulateUntilStable();
        return seaFloor.Iteration;
    }

    public static int PartTwo(string fileName)
    {
        return 0;
    }
}