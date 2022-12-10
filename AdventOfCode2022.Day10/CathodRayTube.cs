namespace AdventOfCode2022.Day10;

public class CathodRayTube
{
    public Task<string[]> GetInput() => File.ReadAllLinesAsync("input.txt");

    public (long sum, bool[,] image) ProcessCommands(string[] commands)
    {
        int currentCycle = 0;
        long x = 1;
        long sum = 0;
        bool[,] image = new bool[6,40];
        foreach(string command in commands)
        {
            if (command == "noop")
            {
                (sum, currentCycle) = Cycle(currentCycle, sum, x, image);
            }
            else 
            {
                int arg = int.Parse(command.Split(' ')[1]);

                (sum, currentCycle) = Cycle(currentCycle, sum, x, image); 
                (sum, currentCycle) = Cycle(currentCycle, sum, x, image);

                x += arg;
            }
        }
        return (sum, image);
    }

    private (long sum, int currentCycle) Cycle(int currentCycle, long sum, long x, bool[,] image)
    {
        int row = currentCycle / 40;
        int column = currentCycle % 40;

        image[row,column] = column == x || column == x - 1 || column == x + 1;

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
