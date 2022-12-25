namespace AdventOfCode2022.Day18;

public class BoilingBoulders
{
    public async Task<Cube[]> GetInput() => (await File.ReadAllLinesAsync("input.txt"))
        .Select(Cube.Parse)
        .ToArray();

    public int GetSurfaceArea(Cube[] cubes)
    {
        int area = 0;

        HashSet<Cube> cubesSet = cubes.ToHashSet();
        foreach (Cube cube in cubesSet)
        {
            if (!cubesSet.Contains(cube with { X = cube.X - 1 }))
            {
                area++;
            }
            if (!cubesSet.Contains(cube with { X = cube.X + 1 }))
            {
                area++;
            }
            if (!cubesSet.Contains(cube with { Y = cube.Y - 1 }))
            {
                area++;
            }
            if (!cubesSet.Contains(cube with { Y = cube.Y + 1 }))
            {
                area++;
            }
            if (!cubesSet.Contains(cube with { Z = cube.Z - 1 }))
            {
                area++;
            }
            if (!cubesSet.Contains(cube with { Z = cube.Z + 1 }))
            {
                area++;
            }
        }

        return area;
    }
}

public record Cube(int X, int Y, int Z)
{
    public static Cube Parse(string line)
    { 
        string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return new Cube(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }
}
