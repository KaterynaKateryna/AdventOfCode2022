using AdventOfCode2022.Day3;

RucksackReorganization rucksackReorganization = new RucksackReorganization();
Rucksack[] input = await rucksackReorganization.GetInput();

// part 1
long sum = rucksackReorganization.GetSumOfMisplacedItems(input);
Console.WriteLine(sum);
