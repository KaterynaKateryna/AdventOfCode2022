namespace AdventOfCode2022.Day11;

public class ItemV1 : IItem
{
    private int _value;

    public ItemV1(int value)
    {
        _value = value;
    }

    public void Divide(int value)
    {
        _value /= value;
    }

    public void Increment(int value)
    {
        _value += value;
    }

    public void Multiply(int value)
    {
        _value *= value;
    }

    public void Power()
    {
        _value *= _value;
    }

    public bool Test(int argument)
    {
        return _value % argument == 0;
    }
}
