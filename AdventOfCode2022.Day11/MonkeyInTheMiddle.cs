namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle
{
    public async Task<Monkey[]> GetInput(int version)
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        Monkey[] monkeys= new Monkey[lines.Length / 7 + 1];
        for (int i = 0; i < lines.Length; i += 7)
        {
            List<int> items = GetPartAfterTheColon(lines[i + 1])
                .Split(",").Select(x => int.Parse(x.Trim())).ToList();

            var @operator = GetNthWordAfterColon(lines[i + 2], 3);
            var argument = GetNthWordAfterColon(lines[i + 2], 4);

            Action<IItem> operation = (item => {
                if (@operator == "*")
                {
                    if (argument == "old")
                    {
                        item.Power();
                    }
                    else
                    {
                        item.Multiply(int.Parse(argument));
                    }
                }
                else
                {
                    item.Increment(int.Parse(argument));
                }
            });

            int testArgument = int.Parse(GetNthWordAfterColon(lines[i + 3], 2));     
            int ifTrue = int.Parse(GetNthWordAfterColon(lines[i + 4], 3));
            int ifFalse = int.Parse(GetNthWordAfterColon(lines[i + 5], 3));

            monkeys[i / 7] = new Monkey(items, operation, testArgument, ifTrue, ifFalse);
        }

        var testArguments = monkeys.Select(m => m.TestArgument).ToList();
        foreach (Monkey monkey in monkeys)
        {
            if (version == 1)
            {
                monkey.Items = monkey.ItemsOriginal.Select(m => new ItemV1(m) as IItem).ToList();
            }
            else
            {
                monkey.Items = monkey.ItemsOriginal.Select(m => new ItemV2(testArguments, m) as IItem).ToList();
            }
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
            IItem item = monkey.Items[0];
            monkey.Items.RemoveAt(0);
            monkey.Operation(item);
            item.Divide(worryDivider);

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
    List<int> ItemsOriginal,
    Action<IItem> Operation,
    int TestArgument,
    int IfTrue,
    int IfFalse
)
{
    public List<IItem> Items { get; set; }

    public long Inspections { get; set; } = 0;

    public Func<IItem, bool> Test = (item => item.Test(TestArgument));
}