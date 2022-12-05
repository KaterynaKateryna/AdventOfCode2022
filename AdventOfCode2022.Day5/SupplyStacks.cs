using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day5;

public class SupplyStacks
{
    public async Task<(Stack<char>[], Move[])> GetInput()
    {
        string[] input = await File.ReadAllLinesAsync("input.txt");

        int empty = 0;
        while (!string.IsNullOrWhiteSpace(input[empty])) { ++empty; }

        int stackCount = input[empty - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        int stackInd = 0;
        Stack<char>[] stacks = new Stack<char>[stackCount];
        for (int i = 0; i < input[empty - 1].Length; ++i)
        {
            if (input[empty - 1][i] == ' ')
            {
                continue;
            }

            stacks[stackInd] = new Stack<char>();
            int j = empty - 2;
            while (j >= 0 && input[j][i] != ' ')
            {
                stacks[stackInd].Push(input[j][i]);
                j--;
            }
            stackInd++;
        }

        Move[] moves = new Move[input.Length - empty - 1];
        for (int i = empty + 1, j = 0; i < input.Length; ++i, ++j)
        {
            moves[j] = Move.Parse(input[i]);
        }

        return (stacks, moves);
    }

    public void PerformMovesOneByOne(Stack<char>[] stacks, Move[] moves)
    {
        foreach (Move move in moves)
        {
            for (int i = 0; i < move.Quantity; ++i)
            {
                char item = stacks[move.From - 1].Pop();
                stacks[move.To - 1].Push(item);
            }
        }
    }

    public void PerformMovesTogether(Stack<char>[] stacks, Move[] moves)
    {
        foreach (Move move in moves)
        {
            Stack<char> temp = new Stack<char>();
            for (int i = 0; i < move.Quantity; ++i)
            {
                char item = stacks[move.From - 1].Pop();
                temp.Push(item);
            }

            while (temp.TryPop(out char item))
            {
                stacks[move.To - 1].Push(item);
            }    
        }
    }

    public string GetTop(Stack<char>[] stacks)
    {
        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }
}

public record Move(int Quantity, int From, int To)
{
    public static Move Parse(string input)
    {
        Match match = Regex.Match(input, "^move ([0-9]+) from ([0-9]+) to ([0-9]+)$");
        int quantity = int.Parse(match.Groups[1].Value); 
        int from = int.Parse(match.Groups[2].Value); 
        int to = int.Parse(match.Groups[3].Value);
        return new Move(quantity, from, to);
    }
}
