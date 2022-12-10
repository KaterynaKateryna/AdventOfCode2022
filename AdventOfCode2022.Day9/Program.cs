using AdventOfCode2022.Day9;

RopeBridge ropeBridge = new RopeBridge();
Move[] moves = await ropeBridge.GetInput();

// part 1
int positions2 = ropeBridge.GetTailPositionCount(moves, 2);
Console.WriteLine(positions2);

// part 2
int positions10 = ropeBridge.GetTailPositionCount(moves, 10);
Console.WriteLine(positions10);
