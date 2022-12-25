using AdventOfCode2022.Day16;

ProboscideaVolcanium volcano = new ProboscideaVolcanium();
Valve start = await volcano.GetInput();

// part 1
int pressure = volcano.GetMaxPressure(30, start);
Console.WriteLine(pressure);