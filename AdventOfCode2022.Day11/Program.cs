using AdventOfCode2022.Day11;
using System.Numerics;

MonkeyInTheMiddle monkeyInTheMiddle = new MonkeyInTheMiddle();

// part 1
Monkey[] monkeys = await monkeyInTheMiddle.GetInput(1);
var level1 = monkeyInTheMiddle.GetMonkeyBusinessLevel(monkeys, 20, 3);
Console.WriteLine(level1);

// part 1
monkeys = await monkeyInTheMiddle.GetInput(2);
var level2 = monkeyInTheMiddle.GetMonkeyBusinessLevel(monkeys, 10000, 1);
Console.WriteLine(level2);
