using System.Collections.Generic;

namespace AdventOfCode2022.Day12;

public class HillClimbingAlgorithm
{
    public async Task<Map> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");

        char[,] grid = new char[lines.Length, lines[0].Length];
        Square start = null, end = null;
        for (int i = 0; i < grid.GetLength(0); ++i)
        {
            for (int j = 0; j < grid.GetLength(1); ++j)
            {
                grid[i, j] = lines[i][j];
                if (grid[i, j] == 'S')
                {
                    start = new Square(i, j);
                    grid[i, j] = 'a';
                }
                else if (grid[i, j] == 'E')
                {
                    end = new Square(i, j);
                    grid[i, j] = 'z';
                }
            }
        }
        return new Map(grid, start, end);
    }

    public int GetShortestRouteBFS(Map map)
    {
        List<Node> toVisit = new List<Node>() { new Node(map.Start, 0) };
        HashSet<Square> visitedSquares = new HashSet<Square>();

        while (toVisit.Any())
        {
            var current = toVisit.First();

            if (current.Square == map.End)
            {
                return current.Step;
            }

            toVisit.RemoveAt(0);
            if (visitedSquares.Add(current.Square))
            {
                List<Square> children = GetPossibleMoves(map, current.Square, visitedSquares);
                toVisit.AddRange(children.Select(c => new Node(c, current.Step + 1)));
            }
        }
        throw new Exception("Path not found");
    }

    private List<Square> GetPossibleMoves(Map map, Square cur, HashSet<Square> visitedSquares)
    {
        List<Square> moves = new List <Square>();
        if (cur.I > 0
            && map.Grid[cur.I - 1, cur.J] - map.Grid[cur.I, cur.J] <= 1
            && !visitedSquares.Contains(cur with { I = cur.I - 1 }))
        {
            moves.Add(cur with { I = cur.I - 1 });
        }

        if (cur.I < map.Grid.GetLength(0) - 1
            && map.Grid[cur.I + 1, cur.J] - map.Grid[cur.I, cur.J] <= 1
            && !visitedSquares.Contains(cur with { I = cur.I + 1 }))
        {
            moves.Add(cur with { I = cur.I + 1 });
        }

        if (cur.J > 0
            && map.Grid[cur.I, cur.J - 1] - map.Grid[cur.I, cur.J] <= 1
            && !visitedSquares.Contains(cur with { J = cur.J - 1 }))
        {
            moves.Add(cur with { J = cur.J - 1 });
        }

        if (cur.J < map.Grid.GetLength(1) - 1
            && map.Grid[cur.I, cur.J + 1] - map.Grid[cur.I, cur.J] <= 1
            && !visitedSquares.Contains(cur with { J = cur.J + 1 }))
        {
            moves.Add(cur with { J = cur.J + 1 });
        }

        return moves;
    }
}

public record Square(int I, int J);

public record Map(char[,] Grid, Square Start, Square End);

public record Node(Square Square, int Step);
