namespace AdventOfCode2022.Day11;

public interface IItem
{
    void Increment(int value);

    void Multiply(int value);

    void Divide(int value);

    void Power();

    bool Test(int argument);
}
