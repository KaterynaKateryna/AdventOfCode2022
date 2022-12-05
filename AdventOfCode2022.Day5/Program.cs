using AdventOfCode2022.Day5;

SupplyStacks supplyStacks = new SupplyStacks();

// part 1
(var stacks, var moves) = await supplyStacks.GetInput();
supplyStacks.PerformMovesOneByOne(stacks, moves);
Console.WriteLine(supplyStacks.GetTop(stacks));

// part 2
(stacks, moves) = await supplyStacks.GetInput();
supplyStacks.PerformMovesTogether(stacks, moves);
Console.WriteLine(supplyStacks.GetTop(stacks));
