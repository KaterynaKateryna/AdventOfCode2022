namespace AdventOfCode2022.Day1;

public class CalorieCounting
{
    public Task<string[]> GetInput() => File.ReadAllLinesAsync("input.txt");

    public long GetMaxCalories(string[] input)
    {
        List<long> calories = GetCalories(input);
        return calories.OrderByDescending(x => x).First();
    }

    public long GetTopThreeMaxCalories(string[] input)
    {
        List<long> calories = GetCalories(input);
        return calories.OrderByDescending(x => x).Take(3).Sum();
    }

    private List<long> GetCalories(string[] input)
    {
        List<long> result = new List<long>();

        long current = 0;
        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                result.Add(current);
                current = 0;
            }
            else
            {
                current += long.Parse(line);
            }
        }
        return result;
    }
}