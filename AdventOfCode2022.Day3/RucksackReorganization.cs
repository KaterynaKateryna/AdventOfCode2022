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
            rucksacks[i] = new Rucksack(items);
        }
        return rucksacks;
    }

    public long GetSumOfMisplacedItems(Rucksack[] input)
    {
        return input.Sum(r => GetMisplacedItem(r));
    }

    private int GetMisplacedItem(Rucksack rucksack)
    {
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

    public long GetSumOfBadges(Rucksack[] input)
    {
        return input.Chunk(3).Sum(ch => GetBadge(ch[0], ch[1], ch[2]));
    }

    public int GetBadge(Rucksack first, Rucksack second, Rucksack third)
    {
        int i = 0, j = 0, k = 0;
        while (first.Items[i] != second.Items[j] || second.Items[j] != third.Items[k])
        {
            if (first.Items[i] <= second.Items[j] && first.Items[i] <= third.Items[k])
            {
                i++;
            }
            else if (second.Items[j] <= first.Items[i] && second.Items[j] <= third.Items[k])
            {
                j++;
            }
            else
            {
                k++;
            }
        }

        return first.Items[i];
    }
}

public record class Rucksack
{
    public Rucksack(int[] items)
    {
        Items = items;
        First = items.Take(items.Length / 2).ToArray();
        Second = items.Skip(items.Length / 2).ToArray();

        Array.Sort(Items);
        Array.Sort(First);
        Array.Sort(Second);
    }

    public int[] Items { get; }

    public int[] First { get; }

    public int[] Second { get; }
}
