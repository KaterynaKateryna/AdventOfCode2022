using AdventOfCode2022.Day2;

RockPaperScissors game = new RockPaperScissors();
Round[] rounds = await game.GetInput();

// part 1
long score1 = game.GetScorePart1(rounds);
Console.WriteLine(score1);

// part 2
long score2 = game.GetScorePart2(rounds);
Console.WriteLine(score2);