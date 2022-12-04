using AdventOfCode2022.Day4;

CampCleanup campCleanup = new CampCleanup();
var input = await campCleanup.GetInput();

// part1
int count = campCleanup.CountFullyContainingPairs(input);
Console.WriteLine(count);
