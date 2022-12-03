namespace AdventOfCode2022.Day2;

public class RockPaperScissors
{
    public async Task<RoundPart1[]> GetInputPart1()
    {
        Round[] result = await GetRawInput();
        return result.Select(r => new RoundPart1((MoveType)r.Them, (MoveType)r.Me)).ToArray();
    }

    public async Task<RoundPart2[]> GetInputPart2()
    {
        Round[] result = await GetRawInput();
        return result.Select(r => new RoundPart2((MoveType)r.Them, (Outcome)r.Me)).ToArray();
    }

    private async Task<Round[]> GetRawInput()
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

    public long GetScorePart1(RoundPart1[] rounds)
    {
        long score = 0;

        foreach (RoundPart1 round in rounds)
        {
            score += GetScore(round.Me, GetOutcome(round.Them, round.Me));
        }

        return score;
    }

    public long GetScorePart2(RoundPart2[] rounds)
    {
        long score = 0;

        foreach (RoundPart2 round in rounds)
        {
            score += GetScore(GetMeShape(round.Them, round.Me), round.Me);
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

public record RoundPart1(MoveType Them, MoveType Me);

public record RoundPart2(MoveType Them, Outcome Me);

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
