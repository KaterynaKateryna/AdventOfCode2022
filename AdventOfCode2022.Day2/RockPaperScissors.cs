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
            int them = (parts[0][0] - 'A');
            int me = (parts[1][0] - 'X');
            result[i] = new Round(them, me);
        }
        return result;
    }

    public long GetScorePart1(Round[] rounds)
    {
        long score = 0;

        foreach (Round round in rounds)
        {
            score += GetScore((MoveType)round.Me, GetOutcome((MoveType)round.Them, (MoveType)round.Me));
        }

        return score;
    }

    public long GetScorePart2(Round[] rounds)
    {
        long score = 0;

        foreach (Round round in rounds)
        {
            score += GetScore(GetMeShape((MoveType)round.Them, (Outcome)round.Me), (Outcome)round.Me);
        }

        return score;
    }
    private long GetScore(MoveType me, Outcome outcome)
    {
        long score = (int)me + 1;

        if (outcome == Outcome.Draw)
        {
            score += 3;
        }
        if (outcome == Outcome.Win)
        {
            score += 6;
        }

        return score;
    }

    private Outcome GetOutcome(MoveType them, MoveType me)
    {
        if (them == me)
            return Outcome.Draw;

        if (me == MoveType.Rock && them == MoveType.Scissors)
            return Outcome.Win;

        if (me == MoveType.Paper && them == MoveType.Rock)
            return Outcome.Win;

        if (me == MoveType.Scissors && them == MoveType.Paper)
            return Outcome.Win;

        return Outcome.Lose;
    }

    private MoveType GetMeShape(MoveType them, Outcome me)
    {
        if (me == Outcome.Draw)
            return them;

        if (me == Outcome.Win)
            return GetWinShape(them);

        if (me == Outcome.Lose)
            return GetLoseShape(them);

        throw new ArgumentOutOfRangeException("outcome not recognised");
    }

    private MoveType GetWinShape(MoveType them)
    {
        switch (them)
        {
            case MoveType.Scissors:
                return MoveType.Rock;
            case MoveType.Rock:
                return MoveType.Paper;
            case MoveType.Paper:
                return MoveType.Scissors;
            default:
                throw new ArgumentOutOfRangeException("move type not recognised");
        }
    }

    private MoveType GetLoseShape(MoveType them)
    {
        switch (them)
        {
            case MoveType.Scissors:
                return MoveType.Paper;
            case MoveType.Rock:
                return MoveType.Scissors;
            case MoveType.Paper:
                return MoveType.Rock;
            default:
                throw new ArgumentOutOfRangeException("move type not recognised");
        }
    }
}

public record Round(int Them, int Me);

public enum MoveType
{
    Rock = 0,
    Paper = 1,
    Scissors = 2
}

public enum Outcome
{
    Lose = 0,
    Draw = 1,
    Win = 2
}
