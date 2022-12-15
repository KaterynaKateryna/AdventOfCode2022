using AdventOfCode2022.Day14;

RegolithReservoir reservoir = new RegolithReservoir();
RockPath[] input = await reservoir.GetInput();

// part 1
int units = reservoir.CountUnits(input, false);
Console.WriteLine(units);

// part 2
int units2 = reservoir.CountUnits(input, true);
Console.WriteLine(units2);
