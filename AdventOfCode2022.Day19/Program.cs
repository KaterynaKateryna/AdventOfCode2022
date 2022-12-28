using AdventOfCode2022.Day19;

NotEnoughMinerals notEnoughMinerals = new NotEnoughMinerals();
Blueprint[] blueprints = await notEnoughMinerals.GetInput();

// part 1
int sum = notEnoughMinerals.GetSumOfQualityLevels(blueprints, 24);
Console.WriteLine(sum);
