namespace AdventOfCode2022.Day6;

public class TuningTrouble
{
    public Task<string> GetInput() => File.ReadAllTextAsync("input.txt");

    public int GetCharsForTheFirstStartOfPacketMarker(string input)
    {
        return GetCharsForTheFirstMarker(input, 4);
    }

    public int GetCharsForTheFirstStartOfMessageMarker(string input)
    {
        return GetCharsForTheFirstMarker(input, 14);
    }

    public int GetCharsForTheFirstMarker(string input, int count)
    {
        for (int i = count - 1; i < input.Length; ++i)
        {
            HashSet<char> temp = new HashSet<char>(count);
            for (int j = 0; j < count; ++j)
            {
                temp.Add(input[i - j]);
            }
            if (temp.Count == count)
            {
                return i + 1;
            }
        }
        throw new InvalidDataException("Marker not found");
    }
}
