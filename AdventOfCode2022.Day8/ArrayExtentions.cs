namespace AdventOfCode2022.Day8;

public static class ArrayExtentions
{
    public static void ForEach(this int[,] array, Action<int, int> action)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                action(i, j);
            }
        }
    }
}
