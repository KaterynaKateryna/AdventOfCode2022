namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle
{
    public async Task<Monkey[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        Monkey[] monkeys= new Monkey[lines.Length / 7 + 1];
        for (int i = 0; i < lines.Length; i += 7)
        {
            List<ulong> items = GetPartAfterTheColon(lines[i + 1])
                .Split(",").Select(x => ulong.Parse(x.Trim())).ToList();

            var @operator = GetNthWordAfterColon(lines[i + 2], 3);
            var argument = GetNthWordAfterColon(lines[i + 2], 4);

            Func<ulong, ulong> operation = (item => {
                ulong arg = argument == "old" ? item : ulong.Parse(argument);
                if (@operator == "*")
                {
                    return item * arg;
                }
                return item + arg;
            });

            ulong testArgument = ulong.Parse(GetNthWordAfterColon(lines[i + 3], 2));
            Func<ulong, bool> test = (item => item % testArgument == 0);

            int ifTrue = int.Parse(GetNthWordAfterColon(lines[i + 4], 3));
            int ifFalse = int.Parse(GetNthWordAfterColon(lines[i + 5], 3));

            monkeys[i / 7] = new Monkey(items, operation, test, ifTrue, ifFalse);
        }
        return monkeys;
    }

    public long GetMonkeyBusinessLevel(Monkey[] monkeys, int rounds, int worryDivider)
    {
        for (int i = 0; i < rounds; ++i)
        {
            Round(monkeys, worryDivider);
        }
        var topMonkeys = monkeys.OrderByDescending(m => m.Inspections).Take(2).ToArray();
        return topMonkeys[0].Inspections * topMonkeys[1].Inspections;
    }

    private void Turn(Monkey[] monkeys, int currentMonkey, int worryDivider)
    {
        Monkey monkey = monkeys[currentMonkey];
        while (monkey.Items.Any())
        {
            monkey.Inspections++;
            ulong item = monkey.Items[0];
            monkey.Items.RemoveAt(0);
            item = monkey.Operation(item) / (ulong)worryDivider;
            if (monkey.Test(item))
            {
                monkeys[monkey.IfTrue].Items.Add(item);
            }
            else
            {
                monkeys[monkey.IfFalse].Items.Add(item);
            }
        }
    }

    private void Round(Monkey[] monkeys, int worryDivider)
    {
        for(int i = 0; i < monkeys.Length; ++i)
        {
            Turn(monkeys, i, worryDivider);
        }
    }

    private string GetNthWordAfterColon(string line, int n)
        => GetPartAfterTheColon(line).Split(' ', StringSplitOptions.RemoveEmptyEntries)[n];

    private string GetPartAfterTheColon(string line)
        => line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1];
}

public record class Monkey(
    List<ulong> Items,
    Func<ulong, ulong> Operation,
    Func<ulong, bool> Test,
    int IfTrue,
    int IfFalse
)
{
    public long Inspections { get; set; } = 0;
}