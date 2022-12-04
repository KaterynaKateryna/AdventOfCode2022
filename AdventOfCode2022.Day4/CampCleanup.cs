namespace AdventOfCode2022.Day4;

public class CampCleanup
{
    public async Task<ElfPair[]> GetInput()
    {
        string[] input = await File.ReadAllLinesAsync("input.txt");
        return input.Select(i => ElfPair.Parse(i)).ToArray();
    }

    public int CountFullyContainingPairs(ElfPair[] input)
    {
        return input.Count(pair => 
            FullyContains(pair.First, pair.Second) || FullyContains(pair.Second, pair.First)
        );
    }

    private bool FullyContains(Assignment first, Assignment second)
    {
        return first.Start <= second.Start && first.End >= second.End;
    }
}

public record class ElfPair(Assignment First, Assignment Second)
{
    public static ElfPair Parse(string input)
    {
        string[] elves = input.Split(',');
        return new ElfPair(Assignment.Parse(elves[0]), Assignment.Parse(elves[1]));
    }
}

public record Assignment(int Start, int End)
{
    public static Assignment Parse(string input)
    {
        string[] sections = input.Split('-');
        return new Assignment(int.Parse(sections[0]), int.Parse(sections[1]));
    }
}
