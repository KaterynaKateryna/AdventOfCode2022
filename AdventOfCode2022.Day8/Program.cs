using AdventOfCode2022.Day8;

TreetopTreeHouse treetopTreeHouse = new TreetopTreeHouse();
int[,] grid = await treetopTreeHouse.GetInput();

// part 1
int count = treetopTreeHouse.GetVisibleTrees(grid);
Console.WriteLine(count);
