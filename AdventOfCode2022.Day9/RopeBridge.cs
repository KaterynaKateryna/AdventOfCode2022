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

    public int GetTailPositionCount(Move[] moves, int length)
    { 
        HashSet<Position> positions = new HashSet<Position>();

        Position[] rope = new Position[length];
        for (int i = 0; i < rope.Length; ++i)
        {
            rope[i] = new Position(0, 0);
            positions.Add(rope[i]);
        }

        foreach (Move move in moves)
        {
            for (int i = 0; i < move.Distance; ++i)
            {
                Move(rope, move.Direction);
                positions.Add(rope[length-1]);
            }
        }

        return positions.Count;
    }

    private void Move(Position[] rope, Direction direction)
    {
        Position head = rope[0];
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
        rope[0] = head;

        for (int i = 0; i < rope.Length - 1; ++i)
        {
            Position previous = rope[i];
            Position tail = rope[i+1];

            int xDiff = Math.Abs(previous.X - tail.X);
            int yDiff = Math.Abs(previous.Y - tail.Y);
            if (xDiff > 1 || yDiff > 1)
            {
                int x = tail.X > previous.X ? tail.X - 1 : tail.X + 1;
                int y = tail.Y > previous.Y ? tail.Y - 1 : tail.Y + 1;
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
            rope[i + 1] = tail;
        }
    }
}

public record Position(int X, int Y);

public record Move(Direction Direction, int Distance);

public enum Direction
{ 
    Up,
    Down,
    Left,
    Right
}


