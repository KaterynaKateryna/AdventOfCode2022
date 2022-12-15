using System.Collections.Generic;

namespace AdventOfCode2022.Day14;

public class RegolithReservoir
{
    public async Task<RockPath[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        RockPath[] input = new RockPath[lines.Length];
        for (int i = 0; i < lines.Length; ++i)
        {
            string[] pointsRaw = lines[i].Split(" -> ");
            Point[] points = new Point[pointsRaw.Length];
            for (int j = 0; j < points.Length; ++j)
            {
                string[] pointRaw = pointsRaw[j].Split(',');
                Point point = new Point(int.Parse(pointRaw[0]), int.Parse(pointRaw[1]));
                points[j] = point;
            }
            input[i] = new RockPath(points);
        }
        return input;
    }

    public int CountUnits(RockPath[] input, bool hasFloor)
    { 
        HashSet<Point> solidPoints = new HashSet<Point>();
        foreach (RockPath path in input)
        {
            solidPoints.UnionWith(path.GetAllPoints());
        }

        int bottomY = solidPoints.Max(p => p.Y);

        int units = 0;
        while (CanPlaceUnit(solidPoints, bottomY, hasFloor))
        {
            units++;
        }
        return units;
    }

    private bool CanPlaceUnit(HashSet<Point> solidPoints, int bottomY, bool hasFloor)
    {
        int x = 500;
        int y = 0;

        if (hasFloor)
        {
            bottomY += 2;
        }

        while (y < bottomY)
        {
            if (hasFloor && y == bottomY - 1)
            {
                return solidPoints.Add(new Point(x, y));
            }

            if (!solidPoints.Contains(new Point(x, y + 1)))
            {
                y++;
            }
            else if (!solidPoints.Contains(new Point(x - 1, y + 1)))
            {
                x--;
                y++;
            }
            else if (!solidPoints.Contains(new Point(x + 1, y + 1)))
            {
                x++;
                y++;
            }
            else
            {
                return solidPoints.Add(new Point(x, y));
            }
        }

        return false;
    }
}

public record Point(int X, int Y);

public record RockPath(Point[] Points)
{
    public HashSet<Point> GetAllPoints()
    {
        HashSet<Point> result = new HashSet<Point>();
        for (int i = 0; i < Points.Length - 1; ++i)
        {
            List<Point> segment = GetSegment(Points[i], Points[i + 1]);
            result.UnionWith(segment);
        }
        return result;
    }

    private List<Point> GetSegment(Point from, Point to)
    {
        List<Point> result = new List<Point>();

        if (from.X == to.X && from.Y < to.Y)
        { 
            for(int i = from.Y; i <= to.Y; ++i) { result.Add(new Point(from.X, i)); }
        }
        if (from.X == to.X && from.Y > to.Y)
        {
            for (int i = to.Y; i <= from.Y; ++i) { result.Add(new Point(from.X, i)); }
        }
        if (from.Y == to.Y && from.X < to.X)
        {
            for (int i = from.X; i <= to.X; ++i) { result.Add(new Point(i, from.Y)); }
        }
        if (from.Y == to.Y && from.X > to.X)
        {
            for (int i = to.X; i <= from.X; ++i) { result.Add(new Point(i, from.Y)); }
        }

        return result;
    }
}
