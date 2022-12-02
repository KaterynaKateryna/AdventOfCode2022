namespace AdventOfCode2022.Day2;

public class RockPaperScissors
{
    public async Task<Round[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        Round[] result = new Round[lines.Length];
        for (int i = 0; i < lines.Length; ++i)
        {
            string[] parts = lines[i].Split(' ');
            MoveType them = (MoveType)(parts[0][0] - 'A'); 
            MoveType me = (MoveType)(parts[1][0] - 'X');
            result[i] = new Round(them, me);
        }
        return result;
    }

    public long GetScore(Round[] rounds)
    {
        long score = 0;

        foreach (Round round in rounds)
        {
            score += (int)round.Me + 1;

            if (round.Me == round.Them)
            {
                score += 3;
            }
            else if (MeWin(round))
            {
                score += 6;
            }
        }

        return score;
    }

    private bool MeWin(Round round)
    {
        if (round.Me == MoveType.Rock && round.Them == MoveType.Scissors)
            return true;
        if (round.Me == MoveType.Paper && round.Them == MoveType.Rock)
            return true;
        if (round.Me == MoveType.Scissors && round.Them == MoveType.Paper)
            return true;
        return false;
    }
}

public record Round(MoveType Them, MoveType Me);

public enum MoveType
{
    Rock = 0,
    Paper = 1,
    Scissors = 2
}
