using AdventOfCode2022.Day13;

DistressSignal distressSignal = new DistressSignal();

// part 1
PacketPair[] pairs = await distressSignal.GetInputPairs();
int sum = distressSignal.SumOfIndicesOfOrderedPairs(pairs);
Console.WriteLine(sum);

// part 2 
Packet[] packets = await distressSignal.GetInputPackets();
int key = distressSignal.GetDecoderKey(packets);
Console.WriteLine(key);