using AdventOfCode2022.Day6;

TuningTrouble tuningTrouble = new TuningTrouble();
string input = await tuningTrouble.GetInput();

// part 1
int answer = tuningTrouble.GetCharsForTheFirstStartOfPacketMarker(input);
Console.WriteLine(answer);

// part 2
int answer2 = tuningTrouble.GetCharsForTheFirstStartOfMessageMarker(input);
Console.WriteLine(answer2);
