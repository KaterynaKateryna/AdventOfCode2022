namespace AdventOfCode2022.Day17;

public class PyroclasticFlow
{
    public async Task<char[]> GetInput() => (await File.ReadAllTextAsync("input.txt")).ToArray();

    public long GetHeightAfterMoves(char[] jets, int rocks)
    {
        int height = 0;
        int currentShape = 0;
        Position currentShapePosition = new Position(2,3);
        int jetPosition = 0;
        int rocksCount = 0;

        List<Position> fixedRocks = new List<Position>();
        while (rocksCount < rocks)
        {
            currentShapePosition = 
                ApplyJet(fixedRocks, currentShape, currentShapePosition, jets[jetPosition]);

            if (CanPlace(fixedRocks, currentShape, currentShapePosition with { Y = currentShapePosition.Y - 1 }))
            {
                currentShapePosition = currentShapePosition with { Y = currentShapePosition.Y - 1 };
            }
            else
            {
                var points = _shapes[currentShape].Offsets.Select(p => p with
                {
                    X = p.X + currentShapePosition.X,
                    Y = p.Y + currentShapePosition.Y
                });
                fixedRocks.AddRange(points);
                currentShape = (currentShape + 1) % _shapes.Length;
                height = fixedRocks.Max(p => p.Y) + 1;
                rocksCount++;

                currentShapePosition = new Position(2,height + 3);
            }

            jetPosition = (jetPosition + 1) % jets.Length;
        }

        return height;
    }

    private Position ApplyJet(
        List<Position> fixedRocks,
        int currentShape,
        Position currentShapePosition,
        char jet
    )
    {
        if (
            jet == '>' && 
            CanPlace(fixedRocks, currentShape, currentShapePosition with { X = currentShapePosition.X + 1 })
        )
        {
            return currentShapePosition with { X = currentShapePosition.X + 1 };
        }
        else if (
            jet == '<'&&
            CanPlace(fixedRocks, currentShape, currentShapePosition with { X = currentShapePosition.X - 1 })
        )
        {
            return currentShapePosition with { X = currentShapePosition.X - 1 };
        }

        return currentShapePosition;
    }

    private bool CanPlace(
        List<Position> fixedRocks,
        int currentShape,
        Position position
    )
    {
        Shape shape = _shapes[currentShape];

        if (position.X < 0 || position.X + shape.Width > 7 || position.Y < 0)
        {
            return false;
        }

        foreach (Position offset in shape.Offsets)
        {
            var checkPosition = position with 
            { 
                X = position.X + offset.X, 
                Y = position.Y + offset.Y 
            };
            if (fixedRocks.Contains(checkPosition))
            {
                return false;
            }
        }

        return true;
    }

    private static Shape[] _shapes = new Shape[5]
    {
        new Shape(
            new Position[4]
            {
                // ####
                new Position(0, 0),
                new Position(1, 0),
                new Position(2, 0),
                new Position(3, 0)
            }
        ),
        new Shape(
            new Position[5]
            { 
                // .#.
                // ###
                // .#.
                new Position(1, 0),
                new Position(0, 1),
                new Position(1, 1),
                new Position(2, 1),
                new Position(1, 2)
            }
        ),
        new Shape(
            new Position[5]
            {
                // ..#
                // ..#
                // ###
                new Position(0, 0),
                new Position(1, 0),
                new Position(2, 0),
                new Position(2, 1),
                new Position(2, 2)
            }
        ),
        new Shape(
            new Position[4]
            { 
                // #
                // #
                // #
                // #
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3)
            }
        ),
        new Shape(
            new Position[4]
            {
                // ##
                // ##
                new Position(0, 0),
                new Position(1, 0),
                new Position(0, 1),
                new Position(1, 1)
            }
        )
    };
}

public record Position(int X, int Y);

public record Shape(Position[] Offsets)
{
    private int _width = Offsets.Max(o => o.X) + 1;

    public int Width { get { return _width; } }
}
