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
        HashSet<int> points = new HashSet<int>();
        foreach (Sensor sensor in sensors)
        {
            int distanceToBeacon =
                Math.Abs(sensor.Position.X - sensor.NearestBeacon.X) +
                Math.Abs(sensor.Position.Y - sensor.NearestBeacon.Y);

            int distanceToY = Math.Abs(sensor.Position.Y - y);

            if (distanceToBeacon >= distanceToY)
            {
                int rest = distanceToBeacon - distanceToY;
                int start = sensor.Position.X - rest;
                int end = sensor.Position.X + rest;
                for (int i = start; i <= end; ++i)
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
}

public record Point(int X, int Y);

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
}
