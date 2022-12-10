using AdventOfCode2022.Day10;

CathodRayTube cathodRayTube = new CathodRayTube();
string[] input = await cathodRayTube.GetInput();

// part 1
long sum = cathodRayTube.GetSumOfSignalStrengths(input);
Console.WriteLine(sum);
