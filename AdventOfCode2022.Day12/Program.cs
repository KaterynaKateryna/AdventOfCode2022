using AdventOfCode2022.Day12;

HillClimbingAlgorithm hillClimbingAlgorithm = new HillClimbingAlgorithm();
Map map = await hillClimbingAlgorithm.GetInput();

// part 1
int route1 = hillClimbingAlgorithm.GetShortestRoute1(map);
Console.WriteLine(route1);

// part 2
int route2 = hillClimbingAlgorithm.GetShortestRoute2(map);
Console.WriteLine(route2);