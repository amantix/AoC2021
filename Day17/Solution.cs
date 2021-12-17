namespace Day17;

public static class Solution
{
    //Just return the value of arithmetic series from 1 to |yMin| using Gauss's formula (again..)
    public static int PartOne(int xMin, int xMax, int yMin, int yMax)
    {
        return -yMin * (-yMin - 1) / 2;
    }

    public static int PartTwo(int xFrom, int xTo, int yFrom, int yTo)
    {
        return ProbesWithinTargetArea(xFrom, xTo, yFrom, yTo);
    }

    //Brute force initial velocities
    //vx: [0..xMax], vy: [yMin..-yMin] assuming than yMin is negative
    private static int ProbesWithinTargetArea(int xMin, int xMax, int yMin, int yMax)
    {
        var probesWithinTargetArea = 0;
        for (var initialVelocityX = 0; initialVelocityX <= xMax; initialVelocityX++)
        {
            for (var initialVelocityY = yMin; initialVelocityY <= -yMin; initialVelocityY++)
            {
                var currentPosition = (x: 0, y: 0);
                var currentVelocity = (vx: initialVelocityX, vy: initialVelocityY);
                var withinTargetArea = false;
                while (currentPosition.x < xMax && currentPosition.y > yMin && !withinTargetArea)
                {
                    currentPosition = (currentPosition.x + currentVelocity.vx, currentPosition.y + currentVelocity.vy);
                    currentVelocity = (currentVelocity.vx - Math.Sign(currentVelocity.vx), currentVelocity.vy - 1);
                    if (currentPosition.x >= xMin
                        && currentPosition.x <= xMax
                        && currentPosition.y >= yMin
                        && currentPosition.y <= yMax
                    )
                    {
                        withinTargetArea = true;
                    }
                }

                if (withinTargetArea)
                {
                    probesWithinTargetArea++;
                }
            }
        }

        return probesWithinTargetArea;
    }
}