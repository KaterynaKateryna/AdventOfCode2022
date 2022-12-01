using AdventOfCode2022.Day1;

CalorieCounting counter = new CalorieCounting();

string[] input = await counter.GetInput();
long answer1 = counter.GetMaxCalories(input);

Console.WriteLine(answer1);
