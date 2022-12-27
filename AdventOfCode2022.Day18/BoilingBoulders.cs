namespace AdventOfCode2022.Day18;

public class BoilingBoulders
{
    public async Task<Cube[]> GetInput() => (await File.ReadAllLinesAsync("input.txt"))
        .Select(Cube.Parse)
        .ToArray();

    public int GetSurfaceArea(Cube[] cubes, bool withInsidePockets)
    {
        List<Cube> cubesSet = GetNeighbours(cubes);

        int area = 0;

        foreach (Cube cube in cubes)
        {
            area += (6 - cube.Neighbours.Count);
        }

        if (!withInsidePockets)
        {
            List<Cube> emptyCubes = cubesSet.Where(c => c.Empty).ToList();
            List<Cube> outsideCubes = GetOutsideCubes(emptyCubes);
            HashSet<Cube> insideCubes = emptyCubes.Except(outsideCubes).ToHashSet();

            foreach (Cube cube in insideCubes)
            {
                area -= cube.Neighbours.Count;
            }
        }

        return area;
    }

    private List<Cube> GetOutsideCubes(List<Cube> cubes)
    {
        Cube start = cubes.Single(c => c.X == - 1 && c.Y == -1 && c.Z == -1);

        List<Cube> visited = new List<Cube>();
        List<Cube> queue = new List<Cube>();

        visited.Add(start);
        queue.AddRange(start.EmptyNeighbours);

        while (queue.Any())
        {
            Cube c = queue[0];
            queue.RemoveAt(0);
            if (visited.Contains(c))
            {
                continue;
            }
            visited.Add(c);

            queue.AddRange(c.EmptyNeighbours);
        }

        return visited;
    }

    private List<Cube> GetNeighbours(Cube[] cubes)
    {
        List<Cube> result = cubes.ToList();
        for (int i = -1; i <= 21; ++i)
        {
            for (int j = -1; j <= 21; ++j)
            {
                for (int k = -1; k <= 21; ++k)
                {
                    if (!cubes.Contains(new Cube(i, j, k, false)))
                    {
                        result.Add(new Cube(i, j, k, true));
                    }
                }
            }
        }

        foreach (Cube cube in result)
        {
            var neighbour = result.SingleOrDefault(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z);
            cube.AddNeighbour(neighbour);

            neighbour = result.SingleOrDefault(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z);
            cube.AddNeighbour(neighbour);

            neighbour = result.SingleOrDefault(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z);
            cube.AddNeighbour(neighbour);

            neighbour = result.SingleOrDefault(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z);
            cube.AddNeighbour(neighbour);

            neighbour = result.SingleOrDefault(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1);
            cube.AddNeighbour(neighbour);

            neighbour = result.SingleOrDefault(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1);
            cube.AddNeighbour(neighbour);
        }

        return result;
    }
}

public record Cube(int X, int Y, int Z, bool Empty)
{
    public static Cube Parse(string line)
    { 
        string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return new Cube(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), false);
    }

    public HashSet<Cube> Neighbours { get; set; } = new HashSet<Cube>();

    public HashSet<Cube> EmptyNeighbours { get; set; } = new HashSet<Cube>();

    public void AddNeighbour(Cube neighbour)
    {
        if (neighbour == null)
        {
            return;
        }

        if (neighbour.Empty)
        {
            EmptyNeighbours.Add(neighbour);
        }
        else
        {
            Neighbours.Add(neighbour);
        }
    }

    public virtual bool Equals(Cube? other)
    { 
        if(other == null) return false;

        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode() =>
        X.GetHashCode() * 23 + Y.GetHashCode() * 17 + Z.GetHashCode();
}
