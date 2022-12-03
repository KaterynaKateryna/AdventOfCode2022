using AdventOfCode2022.Day2;

RockPaperScissors game = new RockPaperScissors();

// part 1
RoundPart1[] rounds1 = await game.GetInputPart1();
long score1 = game.GetScorePart1(rounds1);
Console.WriteLine(score1);

// part 2
RoundPart2[] rounds2 = await game.GetInputPart2();
long score2 = game.GetScorePart2(rounds2);
Console.WriteLine(score2);