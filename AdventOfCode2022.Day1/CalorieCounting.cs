namespace AdventOfCode2022.Day1;

public class CalorieCounting
{
    public Task<string[]> GetInput() => File.ReadAllLinesAsync("input.txt");

    public long GetMaxCalories(string[] input)
    {
        long max = 0;
        long current = 0;
        foreach(string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {   
                if (current > max)
                {
                    max = current;
                }
                current = 0;
            }
            else
            {
                current += long.Parse(line);
            }
        }
        return max;
    }
}