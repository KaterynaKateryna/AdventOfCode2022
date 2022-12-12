namespace AdventOfCode2022.Day11;

public class ItemV2 : IItem
{
    private Dictionary<int, int> _remainders = new Dictionary<int, int>();

    public ItemV2(List<int> testArguments, int value)
    {
        foreach (int arg in testArguments)
        {
            int remainder = value % arg;
            _remainders.Add(arg, remainder);
        }
    }

    public void Increment(int value)
    {
        foreach (KeyValuePair<int, int> map in _remainders)
        {
            _remainders[map.Key] = (map.Value + value) % map.Key;
        }
    }

    public void Multiply(int value)
    {
        foreach (KeyValuePair<int, int> map in _remainders)
        {
            _remainders[map.Key] = (map.Value * value) % map.Key;
        }
    }

    public void Divide(int value)
    {
        // worry levels are not divided
    }

    public void Power()
    {
        foreach (KeyValuePair<int, int> map in _remainders)
        {
            _remainders[map.Key] = (map.Value * map.Value) % map.Key;
        }
    }

    public bool Test(int argument)
    {
        return _remainders[argument] == 0;
    }
}
