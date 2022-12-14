namespace AdventOfCode2022.Day13;

public class DistressSignal
{
    public async Task<PacketPair[]> GetInputPairs()
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

    public async Task<Packet[]> GetInputPackets()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        int packetCount = lines.Length / 3 * 2;
        Packet[] packets = new Packet[packetCount + 2];
        for (int i = 0; i < lines.Length; i += 3)
        {
            packets[i / 3 * 2] = Packet.Parse(lines[i]);
            packets[i / 3 * 2 + 1] = Packet.Parse(lines[i+1]);
        }

        packets[packets.Length - 2] = Packet.Parse("[[2]]");
        packets[packets.Length - 1] = Packet.Parse("[[6]]");

        return packets;
    }

    public int SumOfIndicesOfOrderedPairs(PacketPair[] pairs)
    {
        return pairs
            .Where(p => p.Left.Root.CompareTo(p.Right.Root) <= 0)
            .Sum(p => p.Index);
    }

    public int GetDecoderKey(Packet[] packets)
    {
        Array.Sort(packets);

        int indexOfTwo = -1;
        int indexOfSix = -1;
        for (int i = 0; i < packets.Length; ++i)
        {
            if (packets[i].ToString() == "[[2]]")
            {
                indexOfTwo = i + 1;
            }
            else if (packets[i].ToString() == "[[6]]")
            {
                indexOfSix = i + 1;
            }
        }
        return indexOfTwo * indexOfSix;
    }
}

public record class PacketPair(Packet Left, Packet Right, int Index);

public record class Packet(Node Root) : IComparable<Packet>
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

    public int CompareTo(Packet? other)
    {
        if (other == null)
        {
            return 1;
        }
        return this.Root.CompareTo(other.Root);
    }

    public override string ToString()
    {
        return this.Root.ToString();
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

    public override string ToString()
    {
        if (this.Integer.HasValue)
        {
            return this.Integer.Value.ToString();
        }
        return $"[{string.Join(',', this.List.Select(n => n.ToString()))}]";
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

