using AdventOfCode2022.Day17;

PyroclasticFlow pyroclasticFlow = new PyroclasticFlow();
char[] input = await pyroclasticFlow.GetInput();

// part 1
long height = pyroclasticFlow.GetHeightAfterMoves(input, 2022);
Console.WriteLine(height);