using AdventOfCode2022.Day10;

CathodRayTube cathodRayTube = new CathodRayTube();
string[] input = await cathodRayTube.GetInput();

(long sum, bool[,] image) = cathodRayTube.ProcessCommands(input);


// part 1
Console.WriteLine(sum);

// part 2
for (int i = 0; i < image.GetLength(0); ++i)
{
    for (int j = 0; j < image.GetLength(1); ++j)
    {
        Console.Write(image[i, j] ? "#" : ".");
    }
    Console.WriteLine();
}
