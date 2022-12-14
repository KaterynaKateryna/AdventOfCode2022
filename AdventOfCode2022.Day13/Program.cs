using AdventOfCode2022.Day13;

DistressSignal distressSignal = new DistressSignal();
PacketPair[] pairs = await distressSignal.GetInput();

// part 1
int sum = distressSignal.SumOfIndicesOfOrderedPairs(pairs);
Console.WriteLine(sum);