namespace AdventOfCode2022.Day9;

public class RopeBridge
{
    public async Task<Move[]> GetInput()
    {
        string[] input = await File.ReadAllLinesAsync("input.txt");
        Move[] moves = new Move[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            string[] parts = input[i].Split(' ');
            Direction direction = 0;
            switch (parts[0])
            {
                case "L":
                    direction = Direction.Left;
                    break;
                case "R":
                    direction = Direction.Right;
                    break;
                case "U":
                    direction = Direction.Up;
                    break;
                case "D":
                    direction = Direction.Down;
                    break;
            }
            moves[i] = new Move(direction, int.Parse(parts[1]));
        }
        return moves;
    }

    public int GetTailPositionCount(Move[] moves)
    { 
        HashSet<Position> positions = new HashSet<Position>();

        Rope rope = new Rope(new Position(0, 0), new Position(0, 0));
        positions.Add(rope.Tail);

        foreach (Move move in moves)
        {
            for (int i = 0; i < move.Distance; ++i)
            {
                rope = Move(rope, move.Direction);
                positions.Add(rope.Tail);
            }
        }

        return positions.Count;
    }

    private Rope Move(Rope rope, Direction direction)
    {
        Position head = rope.Head;
        Position tail = rope.Tail;

        switch (direction)
        {
            case Direction.Left:
                head = head with { X = head.X - 1 };
                break;
            case Direction.Right:
                head = head with { X = head.X + 1 };
                break;
            case Direction.Up:
                head = head with { Y = head.Y + 1 };
                break;
            case Direction.Down:
                head = head with { Y = head.Y - 1 };
                break;
        }

        int xDiff = Math.Abs(head.X - tail.X);
        int yDiff = Math.Abs(head.Y - tail.Y);
        if (xDiff > 1 || yDiff > 1)
        {
            int x = tail.X > head.X ? tail.X - 1 : tail.X + 1;
            int y = tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1;
            if (xDiff == 0)
            {
                tail = tail with { Y = y };
            }
            else if (yDiff == 0)
            {
                tail = tail with { X = x };
            }
            else
            {
                tail = new Position(x, y);
            }
        }

        return new Rope(head, tail);
    }
}

public record Position(int X, int Y);

public record Rope(Position Head, Position Tail);

public record Move(Direction Direction, int Distance);

public enum Direction
{ 
    Up,
    Down,
    Left,
    Right
}


