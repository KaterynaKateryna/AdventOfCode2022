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

    public int GetShortestRoute1(Map map)
    {
        return GetShortestRouteBFS(map.Grid, map.Start, new HashSet<Square> { map.End });
    }

    public int GetShortestRoute2(Map map)
    {
        var starts = new HashSet<Square>();
        for (int i = 0; i < map.Grid.GetLength(0); ++i)
        {
            for (int j = 0; j < map.Grid.GetLength(1); ++j)
            {
                if (map.Grid[i, j] == 'a')
                {
                    starts.Add(new Square(i, j));
                }
            }
        }
        return GetShortestRouteBFS(map.Grid, map.End, starts, backwards: true);
    }

    private int GetShortestRouteBFS(char[,] grid, Square start, HashSet<Square> ends, bool backwards = false)
    {
        List<Node> toVisit = new List<Node>() { new Node(start, 0) };
        HashSet<Square> visitedSquares = new HashSet<Square>();

        while (toVisit.Any())
        {
            var current = toVisit.First();

            if (ends.Contains(current.Square))
            {
                return current.Step;
            }

            toVisit.RemoveAt(0);
            if (visitedSquares.Add(current.Square))
            {
                List<Square> children = GetPossibleMoves(grid, current.Square, visitedSquares, backwards);
                toVisit.AddRange(children.Select(c => new Node(c, current.Step + 1)));
            }
        }
        throw new Exception("Path not found");
    }

    private List<Square> GetPossibleMoves(
        char[,] grid, 
        Square cur, 
        HashSet<Square> visitedSquares,
        bool backwards = false
    )
    {
        List<Square> moves = new List <Square>();
        if (cur.I > 0
            && CanMove(grid[cur.I - 1, cur.J], grid[cur.I, cur.J], backwards)
            && !visitedSquares.Contains(cur with { I = cur.I - 1 }))
        {
            moves.Add(cur with { I = cur.I - 1 });
        }

        if (cur.I < grid.GetLength(0) - 1
            && CanMove(grid[cur.I + 1, cur.J], grid[cur.I, cur.J], backwards)
            && !visitedSquares.Contains(cur with { I = cur.I + 1 }))
        {
            moves.Add(cur with { I = cur.I + 1 });
        }

        if (cur.J > 0
            && CanMove(grid[cur.I, cur.J - 1], grid[cur.I, cur.J], backwards)
            && !visitedSquares.Contains(cur with { J = cur.J - 1 }))
        {
            moves.Add(cur with { J = cur.J - 1 });
        }

        if (cur.J < grid.GetLength(1) - 1
            && CanMove(grid[cur.I, cur.J + 1],  grid[cur.I, cur.J], backwards)
            && !visitedSquares.Contains(cur with { J = cur.J + 1 }))
        {
            moves.Add(cur with { J = cur.J + 1 });
        }

        return moves;
    }

    private bool CanMove(char from, char to, bool backwards)
    {
        if (backwards)
        {
            return to - from <= 1;
        }
        else
        {
            return from - to <= 1;
        }
    }
}

public record Square(int I, int J);

public record Map(char[,] Grid, Square Start, Square End);

public record Node(Square Square, int Step);
