using AdventOfCode2022.Day18;

BoilingBoulders boilingBoulders = new BoilingBoulders();
Cube[] input = await boilingBoulders.GetInput();

// part 1
int area = boilingBoulders.GetSurfaceArea(input, withInsidePockets: true);
Console.WriteLine(area);

// part 2
int areaWithoutPockets = boilingBoulders.GetSurfaceArea(input, withInsidePockets: false);
Console.WriteLine(areaWithoutPockets);

