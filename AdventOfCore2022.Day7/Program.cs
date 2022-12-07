using AdventOfCore2022.Day7;

NoSpaceLeftOnDevice program = new NoSpaceLeftOnDevice();
string[] input = await program.GetInput();

// part 1
var tree = program.BuildTree(input);
long sum = tree.GetSizeOfFoldersAtMost(100000);

Console.WriteLine(sum);
