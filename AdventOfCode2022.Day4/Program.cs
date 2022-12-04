using AdventOfCode2022.Day4;

CampCleanup campCleanup = new CampCleanup();
var input = await campCleanup.GetInput();

// part1
int count1 = campCleanup.CountFullyContainingPairs(input);
Console.WriteLine(count1);

// part2
int count2 = campCleanup.CountOverlappingPairs(input);
Console.WriteLine(count2);
