using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day19;

public class NotEnoughMinerals
{
    public async Task<Blueprint[]> GetInput() => (await File.ReadAllLinesAsync("input.txt"))
        .Select(Blueprint.Parse)
        .ToArray();

    public int GetSumOfQualityLevels(Blueprint[] blueprints, int minutes)
        => blueprints.Sum(b => GetMaxGeodes(b, new BlueprintResult(), minutes) * b.Id);

    private int GetMaxGeodes(Blueprint blueprint, BlueprintResult blueprintResult, int minutes)
    {
        if (minutes == 0)
        {
            return blueprintResult.Resources[ResourceType.Geode];
        }

        (BlueprintResult geodeResult, int gMinutesPassed) 
            = BuildGeodeRobot(blueprint, blueprintResult.Copy(), minutes);
        int geodeMax = GetMaxGeodes(blueprint, geodeResult, minutes - gMinutesPassed);

        (BlueprintResult obsidianResult, int obMinutesPassed) 
            = BuildObsidianRobot(blueprint, blueprintResult.Copy(), minutes);
        int obsidianMax = GetMaxGeodes(blueprint, obsidianResult, minutes - obMinutesPassed);

        (BlueprintResult clayResult, int cMinutesPassed)
            = BuildClayRobot(blueprint, blueprintResult.Copy(), minutes);
        int clayMax = GetMaxGeodes(blueprint, clayResult, minutes - cMinutesPassed);

        (BlueprintResult oreResult, int oreMinutesPassed)
            = BuildOreRobot(blueprint, blueprintResult.Copy(), minutes);
        int oreMax = GetMaxGeodes(blueprint, oreResult, minutes - oreMinutesPassed);

        return new int[] { geodeMax, obsidianMax, clayMax, oreMax }.Max();
    }

    private (BlueprintResult, int) BuildGeodeRobot(Blueprint blueprint, BlueprintResult blueprintResult, int limit)
    {
        int minutesPassed = 0;
        while ((blueprint.GeodeRobotObsidianCost > blueprintResult.Resources[ResourceType.Obsidian]
                || blueprint.GeodeRobotOreCost > blueprintResult.Resources[ResourceType.Ore])
                && minutesPassed < limit)
        {
            blueprintResult.Produce();
            minutesPassed++;
        }

        if (minutesPassed == limit)
        {
            return (blueprintResult, minutesPassed);
        }

        blueprintResult.Resources[ResourceType.Obsidian] -= blueprint.GeodeRobotObsidianCost;
        blueprintResult.Resources[ResourceType.Ore] -= blueprint.GeodeRobotOreCost;

        blueprintResult.Produce();
        minutesPassed++;

        blueprintResult.Robots[ResourceType.Geode]++;

        return (blueprintResult, minutesPassed);
    }

    private (BlueprintResult, int) BuildObsidianRobot(Blueprint blueprint, BlueprintResult blueprintResult, int limit)
    {
        int minutesPassed = 0;
        while ((blueprint.ObsidianRobotClayCost > blueprintResult.Resources[ResourceType.Clay]
            || blueprint.ObsidianRobotOreCost > blueprintResult.Resources[ResourceType.Ore])
            && minutesPassed < limit)
        {
            blueprintResult.Produce();
            minutesPassed++;
        }

        if (minutesPassed == limit)
        {
            return (blueprintResult, minutesPassed);
        }

        blueprintResult.Resources[ResourceType.Clay] -= blueprint.ObsidianRobotClayCost;
        blueprintResult.Resources[ResourceType.Ore] -= blueprint.ObsidianRobotOreCost;

        blueprintResult.Produce();
        minutesPassed++;

        blueprintResult.Robots[ResourceType.Obsidian]++;

        return (blueprintResult, minutesPassed);
    }

    private (BlueprintResult, int) BuildClayRobot(Blueprint blueprint, BlueprintResult blueprintResult, int limit)
    {
        int minutesPassed = 0;
        while (blueprint.ClayRobotCost > blueprintResult.Resources[ResourceType.Ore]
            && minutesPassed < limit)
        {
            blueprintResult.Produce();
            minutesPassed++;
        }

        if (minutesPassed == limit)
        {
            return (blueprintResult, minutesPassed);
        }

        blueprintResult.Resources[ResourceType.Ore] -= blueprint.ClayRobotCost;

        blueprintResult.Produce();
        minutesPassed++;

        blueprintResult.Robots[ResourceType.Clay]++;

        return (blueprintResult, minutesPassed);
    }

    private (BlueprintResult, int) BuildOreRobot(Blueprint blueprint, BlueprintResult blueprintResult, int limit)
    {
        int minutesPassed = 0;
        while (blueprint.OreRobotCost > blueprintResult.Resources[ResourceType.Ore]
            && minutesPassed < limit)
        {
            blueprintResult.Produce();
            minutesPassed++;
        }

        if (minutesPassed == limit)
        {
            return (blueprintResult, minutesPassed);
        }

        blueprintResult.Resources[ResourceType.Ore] -= blueprint.OreRobotCost;

        blueprintResult.Produce();
        minutesPassed++;

        blueprintResult.Robots[ResourceType.Ore]++;

        return (blueprintResult, minutesPassed);
    }
}

public record class Blueprint(
    int Id,
    int OreRobotCost,
    int ClayRobotCost,
    int ObsidianRobotOreCost,
    int ObsidianRobotClayCost,
    int GeodeRobotOreCost,
    int GeodeRobotObsidianCost
)
{
    public static Blueprint Parse(string line)
    {
        string pattern = @"Blueprint ([\-0-9]+): Each ore robot costs ([\-0-9]+) ore. " +
            @"Each clay robot costs ([\-0-9]+) ore. " +
            @"Each obsidian robot costs ([\-0-9]+) ore and ([\-0-9]+) clay. " +
            @"Each geode robot costs ([\-0-9]+) ore and ([\-0-9]+) obsidian.";

        Match match = Regex.Match(line, pattern);

        return new Blueprint(
            int.Parse(match.Groups[1].Value), 
            int.Parse(match.Groups[2].Value),
            int.Parse(match.Groups[3].Value),
            int.Parse(match.Groups[4].Value),
            int.Parse(match.Groups[5].Value),
            int.Parse(match.Groups[6].Value),
            int.Parse(match.Groups[7].Value)
        );
    }
}

public record class BlueprintResult
{
    public BlueprintResult()
    {
        Resources = new Dictionary<ResourceType, int>();
        Resources[ResourceType.Ore] = 0;
        Resources[ResourceType.Clay] = 0;
        Resources[ResourceType.Obsidian] = 0;
        Resources[ResourceType.Geode] = 0;

        Robots = new Dictionary<ResourceType, int>();
        Robots[ResourceType.Ore] = 1;
        Robots[ResourceType.Clay] = 0;
        Robots[ResourceType.Obsidian] = 0;
        Robots[ResourceType.Geode] = 0;
    }

    public Dictionary<ResourceType, int> Resources { get; init; }

    public Dictionary<ResourceType, int> Robots { get; init; }

    public void Produce()
    {
        Resources[ResourceType.Ore] += Robots[ResourceType.Ore];
        Resources[ResourceType.Clay] += Robots[ResourceType.Clay];
        Resources[ResourceType.Obsidian] += Robots[ResourceType.Obsidian];
        Resources[ResourceType.Geode] += Robots[ResourceType.Geode];
    }

    public BlueprintResult Copy()
    {
        return new BlueprintResult()
        {
            Resources = new Dictionary<ResourceType, int>(this.Resources),
            Robots = new Dictionary<ResourceType, int>(this.Robots)
        };
    }

}

public enum ResourceType
{ 
    Ore = 1,
    Clay = 2,
    Obsidian = 3,
    Geode = 4
}