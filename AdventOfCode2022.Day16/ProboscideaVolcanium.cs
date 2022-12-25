using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day16;

public class ProboscideaVolcanium
{
    public async Task<Valve> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
  
        Valve[] valves = new Valve[lines.Length];
        for (int i = 0; i < lines.Length; ++i)
        {
            valves[i] = Valve.Parse(lines[i].Split(";")[0]);
        }
        for (int i = 0; i < lines.Length; ++i)
        {
            valves[i].Tunnels = Valve.ParseTunnels(valves, lines[i].Split(";")[1]);
        }
        for (int i = 0; i < valves.Length; ++i)
        {
            valves[i].CalculateDistances();
        }
        return valves[0];
    }

    public int GetMaxPressure(int minutes, Valve start)
    {
        return GetMaxPressure(minutes, start, 0, new HashSet<string>());
    }

    private int GetMaxPressure(int minutes, Valve current, int pressure, HashSet<string> visited)
    {
        if (minutes <= 0)
        {
            return pressure;
        }

        int max = pressure;
        if (!visited.Contains(current.Name))
        {
            visited.Add(current.Name);
            int currentPressure = current.FlowRate * (minutes - 1);

            foreach (KeyValuePair<Valve, int> tunnel in current.Distances)
            {
                if (!visited.Contains(tunnel.Key.Name))
                {
                    if (current.FlowRate == 0)
                    {
                        int travel = GetMaxPressure(minutes - tunnel.Value, tunnel.Key, pressure, visited);
                        if (travel > max)
                        {
                            max = travel;
                        }
                    }

                    if (current.FlowRate != 0)
                    {
                        int openAndTravel = GetMaxPressure(
                            minutes - tunnel.Value - 1, tunnel.Key, pressure + currentPressure, visited);
                        if (openAndTravel > max)
                        {
                            max = openAndTravel;
                        }
                    }
                }
            }
            visited.Remove(current.Name);
        }

        return max;
    }


}

public record Valve(int FlowRate, string Name)
{
    public List<Valve> Tunnels { get; set; } = new List<Valve>();

    public Dictionary<Valve, int> Distances { get; set; } = new Dictionary<Valve, int>();

    public static Valve Parse(string line)
    {
        Match match = Regex.Match(line, "Valve ([A-Z]+) has flow rate=([0-9]+)");
        string name = match.Groups[1].Value;
        int flowRate = int.Parse(match.Groups[2].Value);
        return new Valve(flowRate, name);
    }

    public static List<Valve> ParseTunnels(Valve[] valves, string line)
    {
        Match match = Regex.Match(line, @"tunnels? leads? to valves? ([A-Z,\s]+)");
        string[] names = match.Groups[1].Value
            .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        return valves.Where(v => names.Contains(v.Name)).ToList();
    }

    public void CalculateDistances()
    {
        List<(Valve, int)> toProcess = this.Tunnels.Select(t => (t, 1)).ToList();

        while (toProcess.Any())
        {
            (Valve valve, int valveDistance) = toProcess[0];
            toProcess.RemoveAt(0);
            if (!Distances.ContainsKey(valve) && valve != this)
            {
                Distances[valve] = valveDistance;
                if (valve.Tunnels.Any())
                {
                    toProcess.AddRange(valve.Tunnels.Select(t => (t, valveDistance + 1)));
                }
            }
        }

        Distances = Distances.Where(kv => kv.Key.FlowRate > 0)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}
