namespace AdventOfCode2022.Day3;

public class RucksackReorganization
{
    public async Task<Rucksack[]> GetInput()
    {
        string[] input = await File.ReadAllLinesAsync("input.txt");

        Rucksack[] rucksacks = new Rucksack[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            int[] items = input[i].Select(x => x >= 97 ? x - 96 : x - 38).ToArray(); // ASCII
            rucksacks[i] = new Rucksack(items.Take(items.Length/2).ToArray(), items.Skip(items.Length / 2).ToArray());
        }
        return rucksacks;
    }

    public long GetSumOfMisplacedItems(Rucksack[] input)
    {
        return input.Sum(r => GetMisplacedItem(r));
    }

    private int GetMisplacedItem(Rucksack rucksack)
    {
        Array.Sort(rucksack.First);
        Array.Sort(rucksack.Second);

        int i = 0, j = 0;
        while (rucksack.First[i] != rucksack.Second[j])
        {
            if (rucksack.First[i] < rucksack.Second[j])
            {
                i++;
            }
            else
            {
                j++;
            }
        }

        return rucksack.First[i];
    }
}

public record class Rucksack(int[] First, int[] Second);
