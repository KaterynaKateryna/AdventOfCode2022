using AdventOfCode2022.Day18;

BoilingBoulders boilingBoulders = new BoilingBoulders();
Cube[] input = await boilingBoulders.GetInput();

// part 1
int area = boilingBoulders.GetSurfaceArea(input);
Console.WriteLine(area);
