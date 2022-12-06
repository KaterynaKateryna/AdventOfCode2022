namespace AdventOfCode2022.Day6;

public class TuningTrouble
{
    public Task<string> GetInput() => File.ReadAllTextAsync("input.txt");

    public int GetCharsForTheFirstStartOfPacketMarker(string input)
    {
        for (int i = 3; i < input.Length; ++i)
        {
            HashSet<char> temp = new HashSet<char>(4);
            temp.Add(input[i]);
            temp.Add(input[i-1]);
            temp.Add(input[i-2]);
            temp.Add(input[i-3]);
            if (temp.Count == 4)
            {
                return i + 1;
            }
        }
        throw new InvalidDataException("Start-of=packet marker not found");
    }
}
