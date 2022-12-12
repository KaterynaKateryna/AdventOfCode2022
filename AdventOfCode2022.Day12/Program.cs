using AdventOfCode2022.Day12;

HillClimbingAlgorithm hillClimbingAlgorithm = new HillClimbingAlgorithm();
Map map = await hillClimbingAlgorithm.GetInput();

// part 1
int route = hillClimbingAlgorithm.GetShortestRouteBFS(map);
Console.WriteLine(route);