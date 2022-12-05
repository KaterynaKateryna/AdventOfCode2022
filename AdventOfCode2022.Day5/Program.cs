using AdventOfCode2022.Day5;

SupplyStacks supplyStacks = new SupplyStacks();
(var stacks, var moves) = await supplyStacks.GetInput();

// part 1
supplyStacks.PerformMoves(stacks, moves);
Console.WriteLine(supplyStacks.GetTop(stacks)); 
