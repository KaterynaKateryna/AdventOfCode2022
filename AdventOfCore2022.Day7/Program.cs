using AdventOfCore2022.Day7;

NoSpaceLeftOnDevice program = new NoSpaceLeftOnDevice();
string[] input = await program.GetInput();
var tree = program.BuildTree(input);

// part 1
long sum = tree.GetSizeOfFoldersAtMost(100000);
Console.WriteLine(sum);

// part 2
long size = tree.GetSizeOfFolderToDelete();
Console.WriteLine(size);
