using AdventOfCode2022.Day9;

RopeBridge ropeBridge = new RopeBridge();
Move[] moves = await ropeBridge.GetInput();

// part 1
int positions = ropeBridge.GetTailPositionCount(moves);
Console.WriteLine(positions);
