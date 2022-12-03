using AdventOfCode2022.Day3;

RucksackReorganization rucksackReorganization = new RucksackReorganization();
Rucksack[] input = await rucksackReorganization.GetInput();

// part 1
long sumOfMisplacedItems = rucksackReorganization.GetSumOfMisplacedItems(input);
Console.WriteLine(sumOfMisplacedItems);

// part 2
long sumOfBadges = rucksackReorganization.GetSumOfBadges(input);
Console.WriteLine(sumOfBadges);
