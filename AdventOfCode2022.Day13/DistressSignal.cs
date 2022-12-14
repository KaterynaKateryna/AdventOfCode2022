namespace AdventOfCode2022.Day13;

public class DistressSignal
{
    public async Task<PacketPair[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");

        int pairCount = lines.Length / 3;
        PacketPair[] pairs = new PacketPair[pairCount];
        for (int i = 0; i < lines.Length; i += 3)
        {
            Packet left = Packet.Parse(lines[i]);
            Packet right = Packet.Parse(lines[i+1]);
            pairs[i / 3] = new PacketPair(left, right, i / 3 + 1);
        }
        return pairs;
    }

    public int SumOfIndicesOfOrderedPairs(PacketPair[] pairs)
    {
        return pairs.Where(p => IsOrdered(p)).Sum(p => p.Index);
    }

    private bool IsOrdered(PacketPair pair)
    {
        return pair.Left.Root.CompareTo(pair.Right.Root) <= 0;
    }
}

public record class PacketPair(Packet Left, Packet Right, int Index);

public record class Packet(Node Root)
{
    public static Packet Parse(string line)
    {
        Node root = new Node(null, new List<Node>(), null);
        line = line.Substring(1, line.Length - 2);

        Node current = root;

        for (int i = 0; i < line.Length; ++i)
        {
            switch (line[i])
            {
                case '[':
                    Node node = new Node(null, new List<Node>(), current);
                    current.List.Add(node);
                    current = node;
                    break;
                case ']':
                    current = current.Parent;
                    break;
                case ',':
                    continue;
                default:
                    int integer = i < line.Length - 1
                        && line[i + 1] != '['
                        && line[i + 1] != ']'
                        && line[i + 1] != ',' ?
                    int.Parse(line.Substring(i, 2)) :
                    int.Parse(line.Substring(i, 1));
                    current.List.Add(new Node(integer, new List<Node>(), current));
                    break;
            }
        }

        return new Packet(root);
    }
}

public record class Node(int? Integer, List<Node> List, Node Parent) : IComparable<Node>
{
    public int CompareTo(Node? other)
    {
        if (other == null)
        {
            return 1;
        }
        if (this.Integer.HasValue && other.Integer.HasValue)
        {
            return this.Integer.Value - other.Integer.Value;
        }
        else if (!this.Integer.HasValue && !other.Integer.HasValue)
        {
            for (int i = 0; i < this.List.Count; i++)
            {
                if (i >= other.List.Count)
                {
                    return 1;
                }
                int res = this.List[i].CompareTo(other.List[i]);
                if (res != 0)
                {
                    return res;
                }
            }
            if (other.List.Count == this.List.Count)
            {
                return 0;
            }
            return -1;
        }
        else
        {
            if (this.Integer.HasValue)
            {
                return TransformIntegerToList(this).CompareTo(other);
            }
            return this.CompareTo(TransformIntegerToList(other));
        }
    }

    private Node TransformIntegerToList(Node node)
    { 
        return node with 
        { 
            Integer = null, 
            List = new List<Node> 
            { 
                new Node(node.Integer, new List<Node>(), node.Parent) 
            } 
        };
    }
}

