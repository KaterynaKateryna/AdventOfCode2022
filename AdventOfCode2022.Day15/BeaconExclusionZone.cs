using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day15;

public class BeaconExclusionZone
{
    public async Task<Sensor[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        return lines.Select(l => Sensor.Parse(l)).ToArray();
    }

    public long CantContainBeacon(Sensor[] sensors, int y)
    {
        HashSet<long> points = new HashSet<long>();
        foreach (Sensor sensor in sensors)
        {
            Interval? interval = sensor.GetCoverageAtY(y);
            if (interval != null)
            {
                for (long i = interval.Start; i <= interval.End; ++i)
                {
                    points.Add(i);
                }
            }
        }

        foreach (Sensor sensor in sensors)
        {
            if (sensor.Position.Y == y)
            {
                points.Remove(sensor.Position.X);
            }
            if (sensor.NearestBeacon.Y == y)
            {
                points.Remove(sensor.NearestBeacon.Y);
            }
        }

        return points.Count;
    }

    public long GetTuningFrequency(Sensor[] sensors, int lowerBound, int upperBound)
    {
        Point location = GetBeaconLocation(sensors, lowerBound, upperBound);
        return location.X * 4000000 + location.Y;
    }

    private Point GetBeaconLocation(Sensor[] sensors, int lowerBound, int upperBound)
    {
        for (int y = lowerBound; y <= upperBound; ++y)
        {
            List<Interval> intervals = new List<Interval>();
            foreach (Sensor sensor in sensors)
            {
                Interval? interval = sensor.GetCoverageAtY(y);
                if (interval != null)
                {
                    intervals.Add(interval with 
                    { 
                        Start = interval.Start < lowerBound ? lowerBound : interval.Start,
                        End = interval.End > upperBound ? upperBound : interval.End,
                    });
                }
            }

            Interval[] sorted = intervals.OrderBy(i => i.Start).ToArray();
            Interval merged = sorted[0];
            for (int i = 1; i < sorted.Length; ++i)
            {
                if (sorted[i].Start <= merged.End + 1)
                {
                    if (sorted[i].End > merged.End)
                    { 
                        merged = merged with { End = sorted[i].End };
                    }
                }
                else
                {
                    return new Point(merged.End + 1, y);
                }
            }
        }

        throw new Exception("Beacon not found");
    }
}

public record Point(long X, long Y);

public record Sensor(Point Position, Point NearestBeacon)
{
    public static Sensor Parse(string line)
    {
        string pattern = @"^Sensor at x=([\-0-9]+), y=([\-0-9]+): closest beacon is at x=([\-0-9]+), y=([\-0-9]+)$";
        Match match = Regex.Match(line, pattern);
        int sensorX = int.Parse(match.Groups[1].Value);
        int sensorY = int.Parse(match.Groups[2].Value);
        int beaconX = int.Parse(match.Groups[3].Value);
        int beaconY = int.Parse(match.Groups[4].Value);
        return new Sensor(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
    }

    public Interval? GetCoverageAtY(int y)
    {
        long distanceToBeacon =
                Math.Abs(Position.X - NearestBeacon.X) +
                Math.Abs(Position.Y - NearestBeacon.Y);

        long distanceToY = Math.Abs(Position.Y - y);

        if (distanceToBeacon >= distanceToY)
        {
            long rest = distanceToBeacon - distanceToY;
            long start = Position.X - rest;
            long end = Position.X + rest;
            return new Interval(start, end);
        }
        return null;
    }
}

public record Interval(long Start, long End);
