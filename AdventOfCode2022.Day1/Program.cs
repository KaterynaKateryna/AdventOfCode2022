using AdventOfCode2022.Day1;

CalorieCounting counter = new CalorieCounting();

string[] input = await counter.GetInput();

// part 1
long answer1 = counter.GetMaxCalories(input);
Console.WriteLine(answer1);

// part 2
long answer2 = counter.GetTopThreeMaxCalories(input);
Console.WriteLine(answer2);
