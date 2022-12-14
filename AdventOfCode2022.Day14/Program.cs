using AdventOfCode2022.Day14;

RegolithReservoir reservoir = new RegolithReservoir();
RockPath[] input = await reservoir.GetInput();

// part 1
int units = reservoir.CountUnits(input);
Console.WriteLine(units);
