using AdventOfCode2022.Day11;

MonkeyInTheMiddle monkeyInTheMiddle = new MonkeyInTheMiddle();
Monkey[] monkeys = await monkeyInTheMiddle.GetInput();

// part 1
int level = monkeyInTheMiddle.GetMonkeyBusinessLevel(monkeys);
Console.WriteLine(level);
