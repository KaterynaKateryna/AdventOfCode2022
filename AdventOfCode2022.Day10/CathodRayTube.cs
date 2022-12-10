namespace AdventOfCode2022.Day10;

public class CathodRayTube
{
    public Task<string[]> GetInput() => File.ReadAllLinesAsync("input.txt");

    public long GetSumOfSignalStrengths(string[] commands)
    {
        int currentCycle = 0;
        long x = 1;
        long sum = 0;
        foreach(string command in commands)
        {
            if (command == "noop")
            {
                (sum, currentCycle) = Cycle(currentCycle, sum, x);
            }
            else 
            {
                int arg = int.Parse(command.Split(' ')[1]);

                (sum, currentCycle) = Cycle(currentCycle, sum, x); 
                (sum, currentCycle) = Cycle(currentCycle, sum, x);

                x += arg;
            }
        }
        return sum;
    }

    private (long sum, int currentCycle) Cycle(int currentCycle, long sum, long x)
    {
        currentCycle++;
        if (
            currentCycle == 20
            || currentCycle == 60
            || currentCycle == 100
            || currentCycle == 140
            || currentCycle == 180
            || currentCycle == 220
        )
        {
            sum += currentCycle * x;
        }
        return (sum, currentCycle);
    }
}
