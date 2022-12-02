using AdventOfCode2022.Day2;

RockPaperScissors game = new RockPaperScissors();

Round[] rounds = await game.GetInput();

// part 1
long score = game.GetScore(rounds);
Console.WriteLine(score);