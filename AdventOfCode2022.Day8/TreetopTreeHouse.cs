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

    private bool IsVisible(int[,] grid, int i, int j)
    {
        return
            IsVisibleLeft(grid, i, j) ||
            IsVisibleRight(grid, i, j) ||
            IsVisibleTop(grid, i, j) ||
            IsVisibleBottom(grid, i, j);
    }

    private bool IsVisibleLeft(int[,] grid, int i, int j)
    {
        for (int k = 0; k < j; k++)
        {
            if (grid[i, k] >= grid[i, j])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVisibleRight(int[,] grid, int i, int j)
    {
        for (int k = grid.GetLength(1) - 1; k > j; k--)
        {
            if (grid[i, k] >= grid[i, j])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVisibleTop(int[,] grid, int i, int j)
    {
        for (int k = 0; k < i; k++)
        {
            if (grid[k, j] >= grid[i, j])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVisibleBottom(int[,] grid, int i, int j)
    {
        for (int k = grid.GetLength(0) - 1; k > i; k--)
        {
            if (grid[k, j] >= grid[i, j])
            {
                return false;
            }
        }
        return true;
    }
}
