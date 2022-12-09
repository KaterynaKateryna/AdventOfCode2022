namespace AdventOfCode2022.Day8;

public class TreetopTreeHouse
{
    public async Task<int[,]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        int[,] grid = new int[lines.Length, lines[0].Length];

        grid.ForEach((i, j) => grid[i, j] = int.Parse(lines[i][j].ToString()));
        return grid;
    }

    public int GetVisibleTrees(int[,] grid)
    {
        int count = 0;
        grid.ForEach((i, j) => count += IsVisible(grid, i, j) ? 1 : 0);
        return count;
    }

    public long GetMaxScenicScore(int[,] grid)
    {
        long max = 0;
        grid.ForEach((i, j) =>
        {
            long score = GetScenicScore(grid, i, j);
            if (score > max)
            {
                max = score;
            }
        });
        return max;
    }

    private bool IsVisible(int[,] grid, int i, int j)
    {
        return
            IsVisibleLeft(grid, i, j) ||
            IsVisibleRight(grid, i, j) ||
            IsVisibleTop(grid, i, j) ||
            IsVisibleBottom(grid, i, j);
    }

    private long GetScenicScore(int[,] grid, int i, int j)
    {
        return
            VisibleLeft(grid, i, j) *
            VisibleRight(grid, i, j) *
            VisibleTop(grid, i, j) *
            VisibleBottom(grid, i, j);
    }

    private bool IsVisible(
        int[,] grid, 
        int i, 
        int j, 
        int start, 
        Func<int, bool> forCondition,
        Func<int, int> increment, 
        Func<int, bool> condition
    )
    {
        for (int k = start; forCondition(k); k = increment(k))
        {
            if (condition(k))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVisibleLeft(int[,] grid, int i, int j)
    {
        return IsVisible(grid, i, j, 0, (k) => k < j, (k) => k+1, (k) => grid[i, k] >= grid[i, j]);
    }

    private bool IsVisibleRight(int[,] grid, int i, int j)
    {
        return IsVisible(grid, i, j, grid.GetLength(1) - 1, (k) => k > j, (k) => k - 1, (k) => grid[i, k] >= grid[i, j]);
    }

    private bool IsVisibleTop(int[,] grid, int i, int j)
    {
        return IsVisible(grid, i, j, 0, (k) => k < i, (k) => k + 1, (k) => grid[k, j] >= grid[i, j]);
    }

    private bool IsVisibleBottom(int[,] grid, int i, int j)
    {
        return IsVisible(grid, i, j, grid.GetLength(0) - 1, (k) => k > i, (k) => k - 1, (k) => grid[k, j] >= grid[i, j]);
    }

    private long VisibleCount(
        int[,] grid, 
        int i, 
        int j, 
        int start,
        Func<int, bool> whileCondition,
        Func<int, int> increment,
        Func<int, bool> condition
    )
    {
        int k = start;
        long count = 0;
        while (whileCondition(k) && condition(k))
        {
            count++;
            k = increment(k);
        }

        if (whileCondition(k))
        {
            count++;
        }

        return count;
    }

    private long VisibleLeft(int[,] grid, int i, int j)
    {
        return VisibleCount(grid, i, j, j - 1, (k) => k >= 0, (k) => k - 1, (k) => grid[i, k] < grid[i, j]);
    }

    private long VisibleRight(int[,] grid, int i, int j)
    {
        return VisibleCount(grid, i, j, j + 1, (k) => k < grid.GetLength(1), (k) => k + 1, (k) => grid[i, k] < grid[i, j]);
    }

    private long VisibleTop(int[,] grid, int i, int j)
    {
        return VisibleCount(grid, i, j, i - 1, (k) => k >= 0, (k) => k - 1, (k) => grid[k, j] < grid[i, j]);
    }

    private long VisibleBottom(int[,] grid, int i, int j)
    {
        return VisibleCount(grid, i, j, i + 1, (k) => k < grid.GetLength(0), (k) => k + 1, (k) => grid[k, j] < grid[i, j]);
    }
}
